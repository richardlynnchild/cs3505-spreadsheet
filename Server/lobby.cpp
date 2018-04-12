#include "lobby.h"
#include "spreadsheet.h"
#include "network_controller.h"
#include <pthread.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <fstream>
#include <sstream>
#include <vector>
#include <utility>
#include <iostream>
#include <string>
#include <unistd.h>
/***********************
    Global Variables
***********************/

pthread_mutex_t list_mutex;

static void* ListenForClients(void* ptr);
static void* Handshake(void* ptr);
bool  CheckForNewClient();
void  InitNewClient(int id);
void Send(int id, std::string message);
void Receive(int id);
void AddToSheetList(std::string filename);
void DeleteFromSheetList(std::string filename);


Lobby::Lobby()
{

  //read spreadsheet names from a text file, and populate the 
  //internal list of spreadsheets
  this->running = true;
  
  std::ifstream in_file;
  std::string file_name = "sheet_list.txt";

  in_file.open(file_name.c_str());

  std::string spreadsheet_name;

  while(!in_file.eof() && !in_file.fail())
  {
    getline(in_file, spreadsheet_name);
    std::cout << "Lobby constructor: adding " << spreadsheet_name << " to sheet_list" << std::endl;
    sheet_list.push_back(spreadsheet_name);
  }

  in_file.close();

}


/**************************
     Helper Methods
**************************/
/*
 * Returns the sheet list of this Lobby
 */
std::vector<std::string> Lobby::GetSheetList(){
  return this->sheet_list;
}

/*
 * Build the string needed to send the 
 * connect_accepted message. This is a list
 * of available spreadsheets.
 */
std::string Lobby::BuildConnectAccepted(){
  std::string message = "connect_accepted ";

  pthread_mutex_lock (&list_mutex);
  std::vector<std::string>::iterator it = sheet_list.begin();
  for(;it != sheet_list.end(); it++){
    message += *it;
    message += "\n";
  }
  message += (char) 3;
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

bool Lobby::IsRunning()
{
  return this->running;
}
   
void Lobby::Start(){
  
  // Start a new thread that continuosly listens and accepts new connections 
  pthread_t listen_thread;
  if (pthread_create(&listen_thread, NULL, NetworkController::ListenForClients, this))
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
