#include "lobby.h"
#include "spreadsheet.h"
#include "network_controller.h"
#include <pthread.h>
#include <fstream>
#include <sstream>
#include <vector>
#include <queue>
#include <utility>
#include <iostream>
#include <string>
#include <set>
#include <unistd.h>


Lobby::Lobby()
{
	if (pthread_mutex_init(&list_mutex, NULL) != 0)
		std::cerr << "mutex init failure" << std::endl;
	
	if (pthread_mutex_init(&new_client_mutex, NULL) != 0)
	  std::cerr << "mutex init failure" << std::endl;

	InitSheetList();
}


/**************************
     Helper Methods
**************************/


void Lobby::InitSheetList()
{
	//read spreadsheet names from a text file, and populate the 
	//internal list of spreadsheets
	std::ifstream in_file;
	std::string file_name = "sheet_list.txt";
	
	in_file.open(file_name.c_str());
	
	std::string spreadsheet_name;
	
	while(!in_file.eof() && !in_file.fail())
	{
	  getline(in_file, spreadsheet_name);
	  std::cout << "Lobby constructor: adding " << spreadsheet_name << " to sheet_list" << std::endl;
	  sheet_list.insert(spreadsheet_name);
	}
	
	in_file.close();
}

/*
 * Returns the sheet list of this Lobby
 */
std::set<std::string> Lobby::GetSheetList(){
  return this->sheet_list;
}

/*
 * Add an Interface to the new_client queue.
 */
void Lobby::AddNewClient(Interface interface){
  pthread_mutex_lock(&new_client_mutex);
  this->new_clients.push(interface);
  pthread_mutex_unlock(&new_client_mutex);
}


/*
 * Build the string needed to send the 
 * connect_accepted message. This is a list
 * of available spreadsheets.
 */
std::string Lobby::BuildConnectAccepted(){
  std::string message = "connect_accepted ";

  pthread_mutex_lock (&list_mutex);
  std::set<std::string>::iterator it = sheet_list.begin();
  for(;it != sheet_list.end(); it++){
    message += *it;
    message += "\n";
  }
  message += (char) 3;
  pthread_mutex_unlock(&list_mutex);

  return message;

}

std::string Lobby::BuildFocus()
{
  std::string message = "focus ";
}

std::string Lobby::BuildUnfocus()
{
  std::string message = "unfocus ";
}

/*
 * Checks for and handles a new client if the new client
 * list is non-empty.
 *
 * Returns true if there was a new client to process, returns
 * false otherwise.
 */
bool Lobby::CheckForNewClient(){
  bool idle = true;
  if(new_clients.size() > 0){
    idle = false;
    Interface new_client = new_clients.front();
    new_clients.pop();
    std::string name = new_client.GetSprdName();
    clients.push_back(new_client); 
    if(spreadsheets.count(name)<1){
      Spreadsheet new_sheet(name);
      spreadsheets.insert(std::pair<std::string,Spreadsheet>(name,new_sheet));
    }
    std::string full_state = spreadsheets[name].GetFullState();
	new_client.StartClientThread(); 
    new_client.PushMessage(LOBBY, full_state);
  } 
  return idle;
}


/*
 * Split the given string by the given delimiter.
 * Returns a vector of sub-strings.
 */
std::vector<std::string> Lobby::SplitString(std::string str, char delim){
  std::stringstream ss(str);
  std::string token;
  std::vector<std::string> tokens;
  while(std::getline(ss,token,delim)){
    tokens.push_back(token);
  }
  return tokens;
}

/*
 * Send a change command with the specified string to the
 * clients of the specified spreadsheet.
 */
void Lobby::SendChangeMessage(std::string message, std::string sheet){
  std::string change = "change ";
  change += message;
  char end = (char) 3;
  change += end;
  std::vector<Interface>::iterator it = clients.begin();
    for(; it != clients.end(); ++it){
      if(it->GetSprdName() == sheet){
        it->PushMessage(LOBBY, change);
      }
    } 
}

/*
 * Processes a single message from a client.
 */

void Lobby::HandleMessage(std::string message, std::string sheet){
  
  //Split the message and get the command
  char delim = ' ';
  std::vector<std::string> tokens = SplitString(message, delim);
  std::string command = tokens[0];

  if(command == "edit"){
    char delim = ':';
    std::vector<std::string> cell = SplitString(tokens[1], delim);
    spreadsheets[sheet].EditSheet(cell[0],cell[1]);
    SendChangeMessage(tokens[1], sheet); 
  }
  else if(command == "undo"){
    std::pair<std::string,std::string> cell = spreadsheets[sheet].Undo();
    std::string message = cell.first;
    message += cell.second;
    SendChangeMessage(message,sheet); 
  }
  else if(command == "revert"){
    std::string message = spreadsheets[sheet].Revert(tokens[1]);
    SendChangeMessage(message,sheet); 
  }
  else if(command == "disconnect"){
     
  }

}

/*
 * Returns true if a client sent a message, returns false
 * if all the client message queues were empty
 */
bool Lobby::CheckForMessages(){
  int messagesHandled = 0;
  std::vector<Interface>::iterator it = clients.begin();
  for(; it != clients.end(); ++it){
    //Pop next message off Interface incoming message queue
    std::string message = it->PullMessage(LOBBY);
    std::string sheet = it->GetSprdName();
    if(message == ""){
      continue;
    }
    else {
      HandleMessage(message, sheet);
      messagesHandled++;
    }

  }
  return messagesHandled > 0;
}


bool Lobby::IsRunning()
{
  return running;
}
   
void Lobby::Start(){

	bool listening = false;
	bool loop_running = false;
	// Start a new thread that continuosly listens and accepts new connections 
	pthread_t listen_thread;
	if (pthread_create(&listen_thread, NULL, NetworkController::ListenForClients, this))
		 std::cerr << "error creating thread for client listener" << std::endl;
	else
		listening = true;
	// Start timer thread for pinging clients
	
	pthread_t main_thread;	
	if (pthread_create(&main_thread, NULL, StartMainThread, this))
		 std::cerr << "error creating main lobby thread" << std::endl;
	else
		loop_running = true;

	running = (listening && loop_running);
}

void Lobby::MainLoop()
{
	// Enter main loop:
	//
	//
	// 1. Check for new clients in the new client queue
	//      - If they exist push a full state message into their interface
	//      - Add them to client list
	     
	bool idle;
	while(running){
	  idle = CheckForNewClient();
	  if(idle){
	    int ten_ms = 10000;
	    usleep(ten_ms); 
	  }
	} 
	// 2. For each client, process incoming messages in a Round Robin fashion
	//      - Get message
	//      - Update spreadsheet object
	//      - Push change command to all client interfaces
	// 
	// 3. Check to see if program should be shutdown
	//
	//
}

void* Lobby::StartMainThread(void* ptr)
{
	Lobby* lobby_ptr = (Lobby*) ptr;
	lobby_ptr->MainLoop();
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
  	running = false;
}
