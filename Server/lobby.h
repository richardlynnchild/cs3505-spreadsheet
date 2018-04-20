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

    bool running;
    std::vector<Interface*> clients;
	std::queue<Interface*> new_clients;
    std::map<std::string, Spreadsheet> spreadsheets;
    std::set<std::string> sheet_list;
	void InitSheetList();
    static void* PingLoop(void* ptr);
    bool CheckForNewClient();
    void InitNewClient(int id);
    std::string ParseSheetList();
    void UpdateSheetList(std::string name);
    void OpenSpreadsheet(std::string filename);

    bool CheckForMessages();
    void HandleMessage(std::string message,std::string sheet, int id);
    std::vector<std::string> SplitString(std::string str, char delim);
    void SendChangeMessage(std::string message, std::string sheet);
    void SendFocusMessage(std::string cell, std::string sheet, int id);
    void SendUnfocusMessage(std::string sheet, int id);
    void SendPingResponse(int id);
    static void* StartMainThread(void* ptr);
    void MainLoop();
    Spreadsheet BuildSheetFromFile(std::string name);


  public:
    Lobby();
    void Start();
    void Shutdown();
    std::string BuildConnectAccepted();
    std::set<std::string> GetSheetList();
    bool IsRunning();
    void AddNewClient(Interface* interface);

};

#endif   
