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
    std::vector<Interface> clients;
    std::map<std::string, Spreadsheet> spreadsheets;
    std::set<std::string> sheet_list;
    std::queue< Interface > new_clients;

	void InitSheetList();
    static void* PingLoop(void* ptr);
    bool CheckForNewClient();
    void InitNewClient(int id);
    std::string ParseSheetList();
    void UpdateSheetList(std::string name);
    void OpenSpreadsheet(std::string filename);
    std::string BuildFocus();
    std::string BuildUnfocus();

    bool CheckForMessages();
    void HandleMessage(std::string message,std::string sheet);
    std::vector<std::string> SplitString(std::string str, char delim);
    void SendChangeMessage(std::string message, std::string sheet);

	static void* StartMainThread(void* ptr);
	void MainLoop();

  public:
    Lobby();
    void Start();
    void Shutdown();
    std::string BuildConnectAccepted();
    std::set<std::string> GetSheetList();
    bool IsRunning();
    void AddNewClient(Interface interface);

};

#endif   
