#include <vector>
#include <sys/socket.h>
#include <stdlib.h>
#include <netinet/in.h>
#include <errno.h>
#include <pthread.h>
#include <iostream>
#include <unistd.h>
#include "network_controller.h"
#include "lobby.h"

const int PORT = 2112;

int NetworkController::CreateListenerSocket()
{ 
    //set socket info/optons variables
    int listener_socket, address_size;
    int socket_type = AF_INET;
    int socket_domain = SOCK_STREAM;
    int level = SOL_SOCKET;
    int options_name = SO_REUSEADDR | SO_REUSEPORT;
    int options_value = 1;
    int options_length = sizeof(options_value);
    unsigned address = INADDR_ANY;
    int port = 2112;
    sockaddr_in address_info;

    //create the interface's socket and set its options.
    if(listener_socket = socket(socket_type, socket_domain, 0) == 0)
    {
        //if there was an error creating the socket
        return -1;
    }

    if (setsockopt(listener_socket, level, options_name, &options_value, options_length))
    {
        //if there was an error setting the socket options
        return -1;
    }

    //set parameters of the address object
    address_info.sin_family = socket_type;
    address_info.sin_addr.s_addr = address;
    address_info.sin_port = htons(port);
    address_size = sizeof(address_info);

    //bind the socket to the address and start the listening process
    //and create a new socket for the client when it connects.
    if(bind(listener_socket, (struct sockaddr *)&address_info, address_size) < 0)
    {
        //if there was an error binding the socket to the address
        return -1;
    }

    return listener_socket;
}


int NetworkController::CreateListenerSocket(__socket_type sockType, int sockDomain, int lvl, int optName, int optVal, int _port, unsigned _address)
{ 
    //set socket info/optons variables
    int listener_socket, address_size;
    int socket_type = sockType;
    int socket_domain = sockDomain;
    int level = lvl;
    int options_name = optName;
    int options_value = optVal;
    int options_length = sizeof(options_value);
    unsigned address = _address;
    int port = _port;
    sockaddr_in address_info;

    //create the interface's socket and set its options.
    if(listener_socket = socket(socket_type, socket_domain, 0) == 0)
    {
        //if there was an error creating the socket
        return -1;
    }

    if (setsockopt(listener_socket, level, options_name, &options_value, options_length))
    {
        //if there was an error setting the socket options
        return -1;
    }

    //set parameters of the address object
    address_info.sin_family = socket_type;
    address_info.sin_addr.s_addr = address;
    address_info.sin_port = htons(port);
    address_size = sizeof(address_info);

    //bind the socket to the address and start the listening process
    //and create a new socket for the client when it connects.
    if(bind(listener_socket, (struct sockaddr *)&address_info, address_size) < 0)
    {
        //if there was an error binding the socket to the address
        return -1;
    }

    return listener_socket;
}


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
  
  while(ptr_lobby->IsRunning())
  {  
    std::cout << "Listening..." << std::endl;
    if (listen(listen_socket, 10) < 0)
    {
      std::cerr << "socket listen failure" << std::endl;
      ptr_lobby->Shutdown();
    }

    if ((new_client_socket = accept(listen_socket, (struct sockaddr *)&address, (socklen_t*)&address_length)) < 0)
    {
      std::cerr << "error accepting new client connection" << std::endl;
      ptr_lobby->Shutdown();
    }
    else
    {
      pthread_t handshake_thread;
      struct socket_data* sdata = new socket_data;
      (*sdata).client_socket = new_client_socket;
      (*sdata).ptr_lobby = ptr_lobby;
      if (pthread_create(&handshake_thread, NULL, Handshake, sdata))
      {
        std::cerr << "error creating thread for new client connection" << std::endl;
        ptr_lobby->Shutdown();
      }
      pthread_detach(handshake_thread);
    }
  }
}


/*
 * Send the client a list of available spreadsheets and
 * wait for a load command from client. Add this client to
 * the new client list.
 */
void* NetworkController::Handshake(void* ptr){
  struct socket_data* ptr_data = (socket_data*) ptr;
  Lobby* ptr_obj = (*ptr_data).ptr_lobby;
  std::cout << "Created handshake thread" << std::endl;
  int id = (*ptr_data).client_socket;
  std::cout << "The client on this thread is on socket # " << id << std::endl;
  //read(id,buffer,1024);

  
  int buf_size = 2048;
  char buffer[buf_size];
  int msg_size = 2048;
  int buf_next = 0;
  std::cout << "CH: " << buffer[0] << std::endl;
  std::string msg_str;
  
  ClearBuffer(&buffer[0], buf_size);
  do
  {
    msg_size = Receive(id, &(buffer[buf_next]), buf_size - buf_next);
    buf_next += msg_size;
    msg_str = GetMessage(&buffer[0], buf_next);
    buf_next -= msg_str.length();

  } while(msg_str == "");

  std::cout << "Received: " <<  msg_str << std::endl;
  std::cout << "There are " << ptr_obj->GetSheetList().size() << " spreadsheets available" << std::endl; 
  std::string message = ptr_obj->BuildConnectAccepted();
  std::cout << message << std::endl;
  const char* mbuffer = message.c_str();
  send(id, mbuffer, 1024, MSG_EOR);
  //Send(id, message);
  //read(id,buffer,1024);
  std::string name = buffer;

  delete ptr_data;
   
  Interface interface(id,name);
  ptr_obj->AddNewClient(interface);
}

//Sends a specified message to the client.
//void NetworkController::Send(std::string message)
//{
//    //convert string message to char[].
//    int message_length = message.length() + 1;
//    char msg_char[message_length];
//    strcpy(msg_char, message.c_str());
//
//    send(clientSocket_id, msg_char, message_length, 0);
//}

//Reads from the incoming buffer and returns any messages sent from the client.
int NetworkController::Receive(int client_socket_id, char* msg_buf, int buf_size)
{
    int bytes_received = recv(client_socket_id, msg_buf, buf_size, 0);
    if (bytes_received == -1)
    {
      return 0;
    }

    else
    {
      return bytes_received;
    }
}

//helper method that returns a message and removes it from the messages string buffer, only if the message is complete ('\n' found).
std::string NetworkController::GetMessage(char* msg_buf, int buf_end)
{
    std::string message = "";
    for (int i = 0; i < buf_end; i++)
    {
      std::cout << "message[" << i << "]: " << msg_buf[i] << std::endl;
      if (msg_buf[i] == (char) 3)
      {
         CleanBuffer(msg_buf, i+1, buf_end);
         return message;
      }
      message += msg_buf[i];
    }
  //  std::string buffer(message_buffer);
  //  std::string::size_type position = buffer.find(x);
  //  if (position != std::string::npos)
  //  {
  //      std::string return_msg = buffer.substr(0, position);
  //      return return_msg;
  //  }
    return "";
}

//Clears the memory associated with the given char[].
void NetworkController::CleanBuffer(char* buffer, int msg_start, int msg_end)
{
    int i = 0;
    int j = msg_start;
    while (j < msg_end)
    {
       buffer[i] = buffer[j];
       buffer[j] = (char) 0; 
    }
}

void NetworkController::ClearBuffer(char* buffer, int buf_size)
{
    char * array_start = buffer;
    char * array_end = array_start + buf_size;
    std::fill(array_start, array_end, 0);
}
