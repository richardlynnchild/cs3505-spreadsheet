#include <string>
#include <iostream>
#include <cstring>
#include <sys/socket.h>
#include <netinet/in.h>
#include <errno.h>
#include <pthread.h>
#include <signal.h>
#include <unistd.h>
#include "network_controller.h"
#include "lobby.h"
#include "interface.h"
#include "spreadsheet.h"


/*
 * Set up a socket and continuously listen for new
 * clients.
 */
void* NetworkController::ListenForClients(void* ptr){

	Lobby* ptr_lobby = (Lobby*) ptr; 
	int listen_socket;
	sockaddr_in address_info;
	int new_client_socket;
	struct sockaddr_in address;
	int address_length = sizeof(address);

	
	address.sin_family = AF_INET;
	address.sin_addr.s_addr = INADDR_ANY;
	address.sin_port = htons( PORT );


	bool listening = true;	
	if ((listen_socket = socket(AF_INET, SOCK_STREAM | SOCK_NONBLOCK, 0)) == 0)
	{
		std::cerr << "failed to build socket" << std::endl;
		listening = false;
	}
	if (bind(listen_socket, (struct sockaddr *)&address, sizeof(address)) < 0)
	{
		std::cerr << "failed to bind to localhost:" << PORT << std::endl;
		listening = false;
	}
	
	if (listen(listen_socket, 10) < 0)
	{
		std::cerr << "socket listen failure" << std::endl;
		listening = false;
	}

	while(listening)
	{  
		errno = 0;
		if ((new_client_socket = accept(listen_socket, (struct sockaddr *)&address, (socklen_t*)&address_length)) < 0)
		{
			listening = !(errno == EPIPE);
			usleep(10000);
		}

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
		listening = ptr_lobby->IsRunning();
	}

	close(listen_socket);
}


/*
 * Send the client a list of available spreadsheets and
 * wait for a load command from client. Add this client to
 * the new client list.
 */
void* NetworkController::Handshake(void* ptr){

	struct socket_data* ptr_data = (socket_data*) ptr;
	Lobby* ptr_obj = (*ptr_data).ptr_lobby;
	int id = (*ptr_data).client_socket;
	delete ptr_data;

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
	
	if (msg_str == "register ")
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
	SendFullState(interface);
    Spreadsheet* spreadsheet = interface->GetSpreadPointer();
	int socket_id = interface->GetClientSocketID();
	SetSocketTimeout(socket_id);
	int ping_miss = -1;

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

	std::string lobby_msg = "";

	bool recv_idle, send_idle;
    bool connected = true;
	bool forced_disconnect = false;
	while(connected && ping_miss <= 6)
	{
		recv_idle = true;
		send_idle = true;
		
		lobby_msg = interface->PullMessage(CLIENT);
		if (IsPing(lobby_msg))
		{
			ping_miss++;
			SendPing(socket_id);
		}
		else if (IsDisconnect(lobby_msg))
		{
			SendDisconnect(socket_id);
			connected = false;
			break;
		}

		errno = 0;
		// Receive messages from client
		bytes_received = recv(socket_id, &rcv_buf[rcv_buf_next], 
							rcv_buf_size-rcv_buf_next, MSG_NOSIGNAL);
		if (bytes_received > 0)
		{
			rcv_buf_next += bytes_received;
			recv_idle = false;
		}
		else if (errno == EPIPE)
		{
			connected = false;
			break;
		}
		// Push through interface if message is complete
		rcv_str = GetMessage(&rcv_buf[0], rcv_buf_next);
		if (rcv_str == "ping ")
			ReplyPing(socket_id);
		else if (rcv_str == "ping_response ")
			ping_miss = 0;
		else if (rcv_str == "disconnect ")
		{
			connected = false;
			break;
		}
		else
			spreadsheet->HandleMessage(rcv_str, socket_id);

		// Check for outgoing messages
		if (send_msg_size - send_buf_next <= 0)
		{	
			send_msg_size = 0;
			send_buf_next = 0;	
			
			send_msg_size = spreadsheet->GetMessages(socket_id, &send_buf[0], send_buf_size-1);
		}

		// Send any data in buffer
		if (send_msg_size - send_buf_next > 0)
		{
			errno = 0;
			bytes_sent = send(socket_id, &(send_buf[send_buf_next]),
							send_msg_size-send_buf_next, MSG_NOSIGNAL);

			if (bytes_sent > 0)
			{
				send_buf_next += bytes_sent;
				send_idle = false;
			}
			else if (errno == EPIPE)
			{
				connected = false;
				break;
			}
		}

		// Sleep when things get boring
		if (recv_idle && send_idle && connected)
			usleep(10000);
	}

	close(socket_id);
	interface->MarkThreadClosed();
}


void NetworkController::HandlePipeFail(int sig)
{
	std::cout << "Caught SIGPIPE" << std::endl;
}

void NetworkController::SendFullState(Interface* interface)
{
	int id = interface->GetClientSocketID();
	std::string full_state = interface->PullMessage(CLIENT);
	int buf_size = 2048;
	int msg_size = 0;
	int buf_i = 0;
	char buf[buf_size];
	while (true)
	{
		if (msg_size - buf_i <= 0)
		{
			if (full_state != "")
			{
				buf_i = 0;
				if (full_state.length() > buf_size-1)
				{
					std::string sub_str = full_state.substr(0, buf_size-1);
					msg_size = sub_str.length();
					std::strcpy(buf, sub_str.c_str());
					full_state = full_state.substr(buf_size-1);
				}
				else
				{
					std::strcpy(buf, full_state.c_str());
					msg_size = full_state.length();
					full_state = "";
				}
			}
			else break;
		}
	
		if (msg_size - buf_i > 0)
		{
			errno = 0;
			int bytes = send(id, &buf[buf_i], msg_size-buf_i, MSG_NOSIGNAL);
			if (bytes > 0)
				buf_i += bytes;
			else if (errno == EPIPE)
				return;
		}
	}	
}

void NetworkController::SendDisconnect(int socket_id)
{
	char discon_msg[12] = "disconnect ";
	discon_msg[11] = (char) 3;
	int bytes = 0;
	while (bytes < 12)
	{
		errno = 0;
		bytes += send(socket_id, &discon_msg[bytes],12-bytes, MSG_NOSIGNAL);
		if (errno == EPIPE)
			return;
	}
}

void NetworkController::SendPing(int socket_id)
{
	char ping_msg[6] = "ping ";
	ping_msg[5] = (char) 3;
	send(socket_id, &ping_msg[0],6, MSG_NOSIGNAL);
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

bool NetworkController::TestSocket(int socket_id)
{
	int opt_status = 0;
	int error_code = 0;
	socklen_t code_length = sizeof(error_code);
	opt_status = getsockopt(socket_id, SOL_SOCKET, SO_ERROR, &error_code, &code_length);

	if (opt_status != 0)
	{
		std::cout << "socket " << socket_id << std::endl;
		std::cout << strerror(opt_status) << std::endl;
		return false;
	}

	if (error_code != 0)
	{
		std::cout << "socket " << socket_id << std::endl;
		std::cout << strerror(error_code) << std::endl;
		return false;
	}
	return true;
}

void NetworkController::ReplyPing(int socket_id)
{	
	char msg[15] = "ping_response ";
	char ext = (char)3;
	msg[14] = ext;
	int bytes = send(socket_id, &msg, 15, 0);
}

bool NetworkController::IsPing(std::string msg)
{
	std::string ping = "ping ";
	char ext = (char)3;
	ping += ext;
	return msg == ping;
}

bool NetworkController::IsDisconnect(std::string msg)
{
	std::string dis = "disconnect ";
	char ext = (char)3;
	dis += ext;
	return msg == dis;
}
