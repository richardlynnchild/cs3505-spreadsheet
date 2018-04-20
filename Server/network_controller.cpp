#include <string>
#include <iostream>
#include <cstring>
#include <sys/socket.h>
#include <netinet/in.h>
#include <errno.h>
#include <pthread.h>
#include <unistd.h>
#include "network_controller.h"
#include "lobby.h"
#include "interface.h"


/*
 * Set up a socket and continuously listen for new
 * clients.
 */
void* NetworkController::ListenForClients(void* ptr){

	std::cout << "Bootstrapping listener..." << std::endl;
	Lobby* ptr_lobby = (Lobby*) ptr; 
	int listen_socket;
	sockaddr_in address_info;
	int new_client_socket;
	struct sockaddr_in address;
	int address_length = sizeof(address);
	
	address.sin_family = AF_INET;
	address.sin_addr.s_addr = INADDR_ANY;
	address.sin_port = htons( PORT );
	
	if ((listen_socket = socket(AF_INET, SOCK_STREAM, 0)) == 0)
	{
		std::cerr << "failed to build socket" << std::endl;
		ptr_lobby->Shutdown();
	}
	if (bind(listen_socket, (struct sockaddr *)&address, sizeof(address)) < 0)
	{
		std::cerr << "failed to bind to localhost:" << PORT << std::endl;
		ptr_lobby->Shutdown();
	}
	else
	{
		std::cout << "Listener bound to port: " << PORT << std::endl;
	}
	
	if (listen(listen_socket, 10) < 0)
		std::cerr << "socket listen failure" << std::endl;

	while(ptr_lobby->IsRunning())
	{  	
		if ((new_client_socket = accept(listen_socket, (struct sockaddr *)&address, (socklen_t*)&address_length)) < 0)
			std::cerr << "error accepting new client connection" << std::endl;

		else
		{
			pthread_t handshake_thread;
			struct socket_data* sdata = new socket_data;
			(*sdata).client_socket = new_client_socket;
		 	(*sdata).ptr_lobby = ptr_lobby;
		 	if (pthread_create(&handshake_thread, NULL, Handshake, sdata))
		 		std::cerr << "error creating thread for new client connection" << std::endl;

			pthread_detach(handshake_thread);
		}
	}

	close(listen_socket);
}


/*
 * Send the client a list of available spreadsheets and
 * wait for a load command from client. Add this client to
 * the new client list.
 */
void* NetworkController::Handshake(void* ptr){

	std::cout << "Handshake initiated" << std::endl;
	struct socket_data* ptr_data = (socket_data*) ptr;
	Lobby* ptr_obj = (*ptr_data).ptr_lobby;
	int id = (*ptr_data).client_socket;
	delete ptr_data;

	std::cout << "The new client on socket # " << id << std::endl;
	
	int buf_size = 2048;
	char buffer[buf_size];
	int bytes_received = 0;
	int buf_next = 0;
	std::string msg_str;
	
	ClearBuffer(&buffer[0], buf_size);
	do
	{
	  bytes_received = recv(id, &buffer[buf_next], buf_size-buf_next, 0);
	  if (bytes_received > 0)
	  		buf_next += bytes_received;
	
	  msg_str = GetMessage(&buffer[0], buf_next);
	
	} while(msg_str == "" && ptr_obj->IsRunning());	
	
	if (msg_str == "register")
	{
	  	std::string sprd_name = GetSpreadsheetChoice(id, ptr_obj);
	  	if (sprd_name != "")
	  	{
	  		Interface* interface = new Interface(id,sprd_name);
	  		ptr_obj->AddNewClient(interface);
	  		std::cout << "Added client" << std::endl;
	  	}
	}

}

std::string NetworkController::GetSpreadsheetChoice(int socket_id, Lobby* lobby_ptr)
{	
    std::string sprd_list = lobby_ptr->BuildConnectAccepted();
    std::cout << sprd_list << std::endl;

	int send_buf_size = sprd_list.length();
    char* send_buf = new char [send_buf_size+1];
	std::strcpy(send_buf, sprd_list.c_str());
	int send_buf_next = 0;
	int bytes_sent = 0;

	do
	{
    	bytes_sent = send(socket_id, &(send_buf[send_buf_next]),
							send_buf_size-send_buf_next, 0);
		if (bytes_sent > 0)
			send_buf_next += bytes_sent;
	} while(send_buf_next < send_buf_size && lobby_ptr->IsRunning());

	delete [] send_buf;

	int rcv_buf_size = 2048;
	char rcv_buf[rcv_buf_size];
	int bytes_received = 0;
	int rcv_buf_next = 0;
	std::string rcv_str = "";
  
	ClearBuffer(&rcv_buf[0], rcv_buf_size);

	while (rcv_str == "" && lobby_ptr->IsRunning())
	{
		bytes_received = recv(socket_id, &rcv_buf[rcv_buf_next],
								rcv_buf_size-rcv_buf_next, 0);
		if (bytes_received > 0)
			rcv_buf_next += bytes_received;

		rcv_str = GetMessage(&rcv_buf[0], rcv_buf_next);
	}

    std::string cmd_str = rcv_str.substr(0,5);
    if (cmd_str == "load ")
		return rcv_str.substr(5, rcv_str.length() - 5);

	return "";
}

void* NetworkController::ClientCommunicate(void* ptr)
{
	Interface* interface = (Interface*) ptr;
	int socket_id = interface->GetClientSocketID();
	SetSocketTimeout(socket_id);

	int rcv_buf_size = 2048;
	char rcv_buf[rcv_buf_size];
	int rcv_buf_next = 0;
	int bytes_received = 0;
	std::string rcv_str;
	ClearBuffer(&rcv_buf[0], rcv_buf_size);

	int send_buf_size = 2048;
	int send_buf_next = 0;
	char send_buf[send_buf_size];
	int send_msg_size = 0;
	int bytes_sent = 0;
	std::string snd_str = "";

	std::cout << "ClientCommunicate: Pre-loop" << std::endl;
	bool recv_idle, send_idle;
	while(interface->IsActive())
	{
		recv_idle = true;
		send_idle = true;

		// Receive messages from client
		bytes_received = recv(socket_id, &rcv_buf[rcv_buf_next], 
								rcv_buf_size-rcv_buf_next, 0);
		if (bytes_received > 0)
		{
			rcv_buf_next += bytes_received;
			recv_idle = false;
		}

		// Push through interface if message is complete
		rcv_str = GetMessage(&rcv_buf[0], rcv_buf_next);
		if (rcv_str != "")
			interface->PushMessage(CLIENT, rcv_str);

		// Check for outgoing messages
		if (send_msg_size - send_buf_next <= 0)
		{
			send_msg_size = 0;
			send_buf_next = 0;
			if (snd_str == "")
				snd_str = interface->PullMessage(CLIENT);

			if (snd_str != "")
			{
				std::cout << snd_str << std::endl;
				std::cout << snd_str.length() << std::endl;
				if (snd_str.length() > send_buf_size-1)
				{
					std::string sub_str = snd_str.substr(0, send_buf_size-1);
					send_msg_size = sub_str.length();
					std::strcpy(send_buf, sub_str.c_str());
					snd_str = snd_str.substr(send_buf_size-1);
				}
				else
				{
					std::strcpy(send_buf, snd_str.c_str());
					send_msg_size = snd_str.length();
					snd_str = "";
				}
				send_idle = false;
			}	
		}

		// Send any data in buffer
		if (send_msg_size - send_buf_next > 0)
		{
			bytes_sent = send(socket_id, &(send_buf[send_buf_next]),
								send_msg_size-send_buf_next, 0);
			if (bytes_sent > 0)
				send_buf_next += bytes_sent;
		}

		// Sleep when things get boring
		if (recv_idle && send_idle)
			usleep(10000);
	}

	SendDisconnect(socket_id);
	delete interface;
	close(socket_id);
}

void NetworkController::SendDisconnect(int socket_id)
{
	char discon_msg[11] = "disconnect";
	discon_msg[10] = (char) 3;
	int bytes_sent = 0;
	while (bytes_sent < 11)
	{
		bytes_sent += send(socket_id, &(discon_msg[bytes_sent]),
								11-bytes_sent, 0);
	}
}

//helper method that returns a message and removes it from the messages string buffer, only if the message is complete ('\n' found).
std::string NetworkController::GetMessage(char* msg_buf, int &buf_end)
{
    std::string message = "";
    for (int i = 0; i < buf_end; i++)
    {
      if (msg_buf[i] == (char) 3)
      {
         CleanBuffer(msg_buf, i+1, buf_end);
		 buf_end -= i+1;
         return message;
      }
      message += msg_buf[i];
    }

    return "";
}

//Clears the memory associated with the given char[].
void NetworkController::CleanBuffer(char* buffer, int msg_start, int msg_end)
{
    int i = 0;
    int j = msg_start;
    while (i < msg_end)
    {
		if (j < msg_end)
			buffer[i] = buffer[j];
		else
			buffer[i] = (char) 0;
		i++;
		j++;
    }
}

void NetworkController::ClearBuffer(char* buffer, int buf_size)
{
    char * array_start = buffer;
    char * array_end = array_start + buf_size;
    std::fill(array_start, array_end, 0);
}

void NetworkController::SetSocketTimeout(int socket_id)
{
	struct timeval socket_timeout;
	socket_timeout.tv_sec = 1;
	socket_timeout.tv_usec = 0;

	if (setsockopt(socket_id, SOL_SOCKET, SO_RCVTIMEO, (char*)&socket_timeout,
						sizeof(socket_timeout)) < 0)
		std::cerr << "Failed to set socket recv timeout" << std::endl;

	if (setsockopt(socket_id, SOL_SOCKET, SO_SNDTIMEO, (char*)&socket_timeout,
						sizeof(socket_timeout)) < 0)
		std::cerr << "Failed to set socket send timeout" << std::endl;
}
