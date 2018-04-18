#ifndef LOBBY_H
#define LOBBY_H

#include "spreadsheet.h"
#include "interface.h"
#include <queue>
#include <string>
#include <map>
#include <queue>
#include <utility>
#include <sys/socket.h>
#include <pthread.h>
#include "interface.h"

class Lobby {
  private:

    bool running;
    std::vector<Interface> clients;
    std::map<std::string, Spreadsheet> spreadsheets;
    std::vector<std::string> sheet_list;
    std::queue< Interface > new_clients;

    static void* PingLoop(void* ptr);
    bool CheckForNewClient();
    void InitNewClient(int id);
    std::string ParseSheetList();
    static void Send(int socket_id, std::string message);
    void UpdateSheetList(std::string name);
    void OpenSpreadsheet(std::string filename);
    std::string BuildFocus();
    std::string BuildUnfocus();

  public:
    Lobby();
    void Start();
    void Shutdown();
    std::string BuildConnectAccepted();
    std::vector<std::string> GetSheetList();
    bool IsRunning();
    void AddNewClient(Interface interface);

};

#endif   
