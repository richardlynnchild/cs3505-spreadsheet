#include "lobby.h"
#include <pthread.h>
#include <sys/socket.h>
#include <fstream>
#include <sstream>
#include <vector>

void* ListenForClients(void* ptr);
void* Handshake(void* ptr);
bool  CheckForNewClient();
void  InitNewClient(int id);
std::string BuildConnectAccepted();
void Send(int id, std::string message);
void Receive(int id);
void AddToSheetList(std::string filename);
void DeleteFromSheetList(std::string filename);


Lobby::Lobby(int port)
{
  //NOTE: for now this is assuming spreadsheet names are separated by spaces
  //I'm working on getting it okay with parsing names separated by commas if it's not too complicated

  //read spreadsheet names from a text file, and populate the 
  //internal list of spreadsheets
  std::ifstream in_file("sheet_list.txt");

  while(true)
  {
    std::string spreadsheet_name;
    in_file >> spreadsheet_name;

    if(in_file.fail())
      break;
    
    sheet_list.push_back(spreadsheet_name);
  }

  in_file.close();

  this->port = port;
}


**************************
     Helper Methods
**************************

/*
 * Set up a socket and continuously listen for new
 * clients.
 */
void* ListenForClients(void* ptr){
  while(true){
    

  }
}

/*
 * Send the client a list of available spreadsheets and
 * wait for a load command from client. Update list of
 * available spreadsheets if needed. Add this client to
 * the new client list.
 */
void* Handshake(void* ptr){
  

}

/*
 * Build the string needed to send the 
 * connect_accepted message. This is a list
 * of available spreadsheets.
 */
std::string BuildConnectAccepted(){
  
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
   

