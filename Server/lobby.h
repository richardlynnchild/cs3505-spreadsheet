#ifndef LOBBY_H
#define LOBBY_H

#include <string>
#include <map>
#include <queue>
#include <vector>
#include <pthread.h>
#include "spreadsheet.h"
#include "interface.h"
#include <set>


class Lobby {
  private:

	pthread_mutex_t list_mutex;
	pthread_mutex_t new_client_mutex;
    pthread_mutex_t client_list_mutex;

	pthread_t listen_thread;
	pthread_t ping_thread;
	pthread_t main_thread;
	
    bool running;
    std::vector<Interface*> clients;
	std::queue<std::vector<Interface*>::iterator> dead_clients;
    std::queue<Interface*> new_clients;
    std::map<std::string, Spreadsheet*> spreadsheets;
    std::set<std::string> sheet_list;
    void InitSheetList();
    bool CheckForNewClient();
    void InitNewClient(int id);
    std::string ParseSheetList();
    void UpdateSheetList(std::string name);
    void OpenSpreadsheet(std::string filename);

    bool CheckForMessages();
    void HandleMessage(std::string message, std::string sheet, int id);
	void CleanDeadClients();
    //std::vector<std::string> SplitString(std::string str, char delim);
    void SendChangeMessage(std::string message, std::string sheet);
    void SendFocusMessage(std::string cell, std::string sheet, int id);
    void SendUnfocusMessage(std::string sheet, int id);
    //void SendPingResponse(int id);
    //void ResetPingMiss(int id);
    void MainLoop();
    void LobbyPing();
	
    static void* StartMainThread(void* ptr);
    static void* PingLoop(void* ptr);

  public:
    Lobby();
    void Start();
    void Shutdown();
    std::string BuildConnectAccepted();
    bool IsRunning();
    void AddNewClient(Interface* interface);

};

#endif   
