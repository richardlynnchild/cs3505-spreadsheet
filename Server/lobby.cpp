#include "lobby.h"
#include <pthread.h>
#include <sys/socket.h>
#include <fstream>
#include <sstream>
#include <vector>

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


Lobby::Lobby(int port)
{

  //read spreadsheet names from a text file, and populate the 
  //internal list of spreadsheets
  
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
  while(true){
    

  }
}

/*
 * Send the client a list of available spreadsheets and
 * wait for a load command from client. Update list of
 * available spreadsheets if needed. Add this client to
 * the new client list.
 */
void* Lobby::Handshake(void* ptr){
  int id = *(int *) ptr;
  std::string message = BuildConnectAccept();
  Send(id, message);
  recv(id,buffer,1024,NULL);
  std::string name = buffer;
  
   

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

void Send(int id, std::string message){

}

/*
 * Checks for and handles a new client if the new client
 * list is non-empty.
 *
 * Returns true if there was a new client to process, returns
 * false otherwise.
 */
bool CheckForNewClient(){

}

/*
 * Handles a new client by adding them to the subscribed client
 * list and sending them a full-state message.
 */
void InitNewClient(int id){
  
}
   

