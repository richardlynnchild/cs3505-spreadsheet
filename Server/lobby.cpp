#include "lobby.h"
#include "spreadsheet.h"
#include <pthread.h>
#include <sys/socket.h>
#include <fstream>
#include <sstream>
#include <vector>
#include <utility>


/***********************
    Global Variables
***********************/

pthread_mutex_t list_mutex;
char buffer[1024];

static void* ListenForClients(void* ptr);
static void* Handshake(void* ptr);
bool  CheckForNewClient();
void  InitNewClient(int id);
void Send(std::string message, int id);
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

void Start(){

}

void Shutdown(){

}


/**************************
     Helper Methods
**************************/

/*
 * Build the string needed to send the 
 * connect_accepted message. This is a list
 * of available spreadsheets.
 */
std::string Lobby::BuildConnectAccepted(){
  std::string message = "connect_accepted ";

  pthread_mutex_lock (&list_mutex);
  std::vector<std::string>::iterator it = this->sheet_list.begin();
  for(;it != this->sheet_list.end(); it++){
    message += *it;
    message += "\n";
  }
  message += "\3";
  pthread_mutex_unlock(&list_mutex);

  return message;

}


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
  std::string message = BuildConnectAccepted();
  Send(message, id);
  recv(id,buffer,1024,MSG_WAITALL);
  std::string name = buffer;
  
   

}

/*
 * Send the specified message to the specified client.
 */

void Lobby::Send(std::string message, int id){

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
   

