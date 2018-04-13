#ifndef LOBBY_H
#define LOBBY_H

#include "spreadsheet.h"
#include <string>
#include <map>
#include <queue>
#include <utility>
#include <sys/socket.h>
#include <pthread.h>
class Lobby {
  private:
    bool running;
    std::vector<int> clients;
    std::vector<int> new_clients;
    std::map<std::string, Spreadsheet> spreadsheets;
    std::vector<std::string> sheet_list;
    std::queue< std::pair<int,std::string> > new_clients;

    static void* PingLoop(void* ptr);
    bool CheckForNewClient();
    void InitNewClient(int id);
    std::string ParseSheetList();
    static void Send(int socket_id, std::string message);
    void UpdateSheetList(std::string name);
    void OpenSpreadsheet(std::string filename);
    void AddNewClient(int id, std::string name);
  public:
    Lobby();
    void Start();
    void Shutdown();

    std::string BuildConnectAccepted();
    std::vector<std::string> GetSheetList();
    bool IsRunning();

};

#endif   
