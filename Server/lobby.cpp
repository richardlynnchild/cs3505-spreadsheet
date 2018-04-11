#include "lobby.h"
#include <pthread.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <fstream>
#include <sstream>
#include <vector>
#include <iostream>
#include <string>

/***********************
    Global Variables
***********************/

pthread_mutex_t list_mutex;
char buffer[1024];

void* ListenForClients(void* ptr);
void* Handshake(void* ptr);
bool  CheckForNewClient();
void  InitNewClient(int id);
std::string BuildConnectAccept();
void Send(int id, std::string message);
void Receive(int id);
void AddToSheetList(std::string filename);
void DeleteFromSheetList(std::string filename);

struct member_data {
  int* port;
  bool* running;
};

Lobby::Lobby(int port)
{

  //read spreadsheet names from a text file, and populate the 
  //internal list of spreadsheets
  this->running = true;
  
  std::ifstream in_file;
  std::string file_name = "sheet_list.txt";

  in_file.open(file_name.c_str());

  std::string spreadsheet_name;

  while(!in_file.eof())
  {
    getline(in_file, spreadsheet_name);

    sheet_list.push_back(spreadsheet_name);
  }

  in_file.close();

  this->port = port;

}


/**************************
     Helper Methods
**************************/

/*
 * Set up a socket and continuously listen for new
 * clients.
 */
void* Lobby::ListenForClients(void* ptr){
  std::cout << "Bootstrapping listener..." << std::endl;
  int listen_socket;
  struct member_data* data = (struct member_data*) ptr;
  std::cout << *((*data).port) << std::endl;
  int new_client_socket;
  struct sockaddr_in address;
  int address_length = sizeof(address);
  int backlog = 5;

  address.sin_family = AF_INET;
  address.sin_addr.s_addr = INADDR_ANY;
  address.sin_port = htons( *(*data).port);

  if ((listen_socket = socket(AF_INET, SOCK_STREAM, 0)) == 0)
  {
    std::cerr << "failed to build socket" << std::endl;
    *(*data).running = false;
  }
  if (bind(listen_socket, (struct sockaddr *)&address, sizeof(address)) < 0)
  {
    std::cerr << "failed to bind to localhost:" << *(*data).port << std::endl;
    *(*data).running = false;
  }

  std::cout << "Listener bound to port: " << *(*data).port << std::endl;
  
  while(*(*data).running)
  {  
    std::cout << "Listening..." << std::endl;
    if (listen(listen_socket, backlog) < 0)
    {
      std::cerr << "socket listen failure" << std::endl;
      *(*data).running = false;
    }

    if ((new_client_socket = accept(listen_socket, (struct sockaddr *)&address, (socklen_t*)&address_length)) < 0)
    {
      std::cerr << "error accepting new client connection" << std::endl;
      *(*data).running = false;
    }
    else
    {
      pthread_t handshake_thread;
      int* socket_ptr = new int;
      *socket_ptr = new_client_socket;
      if (pthread_create(&handshake_thread, NULL, Handshake, socket_ptr))
      {
        std::cerr << "error creating thread for new client connection" << std::endl;
        *(*data).running = false;
      }
    }
  }
  delete data;
}

/*
 * Send the client a list of available spreadsheets and
 * wait for a load command from client. Update list of
 * available spreadsheets if needed. Add this client to
 * the new client list.
 */
void* Lobby::Handshake(void* ptr){

  int* client_socket = (int*) ptr;
  std::cout << "Created handshake thread" << std::endl;
  int id = *client_socket;
  std::string message = BuildConnectAccept();
  Send(id, message);
  recv(id,buffer,1024,NULL);
  std::string name = buffer;
  
  delete client_socket;
   
}

/*
 * Build the string needed to send the 
 * connect_accepted message. This is a list
 * of available spreadsheets.
 */

std::string Lobby::BuildConnectAccept(){
  std::string message = "connect_accepted ";

  pthread_mutex_lock (&list_mutex);
  std::map<std::string, Spreadsheet>::iterator it = this->spreadsheets.begin();
  for(;it != this->spreadsheets.end(); it++){
    message += it->first;
    message += "\n";
  }
  message += "\3";
  pthread_mutex_unlock(&list_mutex);

  return message;

}

/*
 * Send the specified message to the specified client.
 */

void Lobby::Send(int id, std::string message){

}

/*
 * Checks for and handles a new client if the new client
 * list is non-empty.
 *
 * Returns true if there was a new client to process, returns
 * false otherwise.
 */
bool Lobby::CheckForNewClient(){

}

/*
 * Handles a new client by adding them to the subscribed client
 * list and sending them a full-state message.
 */
void Lobby::InitNewClient(int id){
  
}
   
void Lobby::Start(){
  
  // Start a new thread that continuosly listens and accepts new connections 
  pthread_t listen_thread;
  struct member_data* data = new member_data;
  (*data).port = &(this->port);
  (*data).running = &(this->running);
  if (pthread_create(&listen_thread, NULL, ListenForClients, data))
  {
    std::cerr << "error creating thread for client listener" << std::endl;
    this->running = false;
  }

  // Start timer thread for pinging clients

  // Enter main loop:
  //
  //
  // 1. Check for new clients in the new client queue
  //      - If they exist push a full state message into their interface
  //      - Add them to client list
  //
  // 2. For each client, process incoming messages in a Round Robin fashion
  //      - Get message
  //      - Update spreadsheet object
  //      - Push change command to all client interfaces
  // 
  // 3. Check to see if program should be shutdown
  //
  //

}

void* Lobby::PingLoop(void* ptr)
{
  // loop:
  // 
  // 1. Iterate through clients and push ping message
  // 2. Wait 10 seconds
  // 3. pingMiss++ for each client 
}

void Lobby::Shutdown(){
  this->running = false;
}
