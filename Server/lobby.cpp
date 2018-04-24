
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

	if (pthread_mutex_init(&client_list_mutex, NULL) != 0)
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
	  sheet_list.insert(spreadsheet_name);
	}
	
	in_file.close();
}

/*
 * Add an Interface to the new_client queue.
 */
void Lobby::AddNewClient(Interface* interface){
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

bool Lobby::CheckForNewClient(){
  bool idle = true;
 
  Interface* new_client;
  std::string name;
 
  //Check if there is a client waiting to connect
  if(new_clients.size() > 0){
    pthread_mutex_lock(&new_client_mutex);
    idle = false;
    new_client = new_clients.front();
    new_clients.pop();
    name = new_client->GetSprdName();
    pthread_mutex_unlock(&new_client_mutex);
  }

  if (!idle) {
    Spreadsheet* new_sheet;
    int id = new_client->GetClientSocketID();
    //Check if the desired spreadsheet is active 
    if(spreadsheets.count(name)<1){
      //Check if the spreadsheet is saved
      new_sheet = new Spreadsheet(name);  //not active, not saved
      spreadsheets.insert(std::pair<std::string,Spreadsheet*>(name,new_sheet));
      pthread_mutex_lock(&list_mutex);
      std::set<std::string>::iterator it = sheet_list.find(name);
      if(it == sheet_list.end()){
        sheet_list.insert(name);
      }
      pthread_mutex_unlock(&list_mutex);
    }
    else
    {
      new_sheet = spreadsheets[name];
    }
    std::string full_state = new_sheet->GetFullState(id);
	new_client->SetSpreadPointer(new_sheet);
    new_client->PushMessage(LOBBY, full_state);
    new_client->StartClientThread();
    pthread_mutex_lock(&client_list_mutex);
    clients.push_back(new_client);
    pthread_mutex_unlock(&client_list_mutex);
  } 
  return idle;
}

/*
 * Returns true if a client sent a message, returns false
 * if all the client message queues were empty
 */
bool Lobby::CheckForMessages(){
  int messages_handled = 0;
  std::vector<Interface*>::iterator it = clients.begin();
  for(; it != clients.end(); ++it){
      if ((*it)->IsActive() == false)
        dead_clients.push(it);
  }
  return messages_handled > 0;
}

void Lobby::LobbyPing()
{
  std::string msg = "ping ";
  char end = (char) 3;
  msg += end;
  while(IsRunning())
  {  
    pthread_mutex_lock(&client_list_mutex);
    std::vector<Interface*>::iterator it = clients.begin();
    for(; it!= clients.end(); ++it)
    {
	  int id = (*it)->GetClientSocketID();
      (*it)->PushMessage(LOBBY, msg);
    }
    pthread_mutex_unlock(&client_list_mutex);
    sleep(10);
  }
}

bool Lobby::IsRunning()
{
  return running;
}
   
void Lobby::Start(){

	bool listening = false;
	bool loop_running = false;
	bool pinging = false;

	// Start a new thread that continuosly listens and accepts new connections 
	if (pthread_create(&listen_thread, NULL, NetworkController::ListenForClients, this)){
		 std::cerr << "error creating thread for client listener" << std::endl;
        }
	else
	{
	  listening = true;
	  pthread_detach(listen_thread);
    }   
	// Start timer thread for pinging clients
	
	if (pthread_create(&ping_thread, NULL, PingLoop, this))
		 std::cerr << "error creating thread for pinging" << std::endl;
	else
		pinging = true;
	
	if (pthread_create(&main_thread, NULL, StartMainThread, this)){
		 std::cerr << "error creating main lobby thread" << std::endl;
        }
	else
	  loop_running = true;   
        
	running = (listening && loop_running);
}

void Lobby::MainLoop()
{
	bool new_clients, new_messages;
	while(IsRunning()){
	  new_clients = CheckForNewClient();
	  new_messages = CheckForMessages();
      CleanDeadClients();
      if (!new_clients && !new_messages)
      {
	      int ten_ms = 10000;
	      usleep(ten_ms); 
	  }
	} 
}

void* Lobby::StartMainThread(void* ptr)
{
	Lobby* lobby_ptr = (Lobby*) ptr;
	lobby_ptr->MainLoop();
}

void* Lobby::PingLoop(void* ptr)
{
  Lobby* lobby_ptr = (Lobby*) ptr;
  lobby_ptr->LobbyPing();
}


void Lobby::CleanDeadClients()
{
	if (dead_clients.empty())
		return;

	std::vector<Interface*>::iterator it;
	Interface* interface;
	pthread_mutex_lock(&client_list_mutex);
	while (!dead_clients.empty())
	{
		it = dead_clients.front();
		dead_clients.pop();
		interface = *it;
		interface->StopClientThread();
		clients.erase(it);
		delete interface;
	}
	pthread_mutex_unlock(&client_list_mutex);
}

void Lobby::Shutdown(){
  //stop the lobby main loop
  running = false;

  pthread_join(ping_thread, NULL);
  pthread_join(main_thread, NULL);

  std::cout << "Shutdown initiated..." << std::endl;
  //send a disconnect message
  //to each client
  std::vector<Interface*>::iterator c_it = clients.begin();
  for(; c_it != clients.end(); ++c_it){
    Interface* interface = *c_it;
    interface->StopClientThread();
    delete interface;
  }
  //save each spreadsheet object to disk
  std::map<std::string, Spreadsheet*>::iterator s_it = spreadsheets.begin();
  for(; s_it != spreadsheets.end(); ++s_it){
    std::string filename = s_it->first;
    Spreadsheet* spread = (s_it->second);
    spread->WriteSpreadsheet(filename);
    delete spread;
  }   

}



