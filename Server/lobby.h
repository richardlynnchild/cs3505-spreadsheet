#ifndef LOBBY_H
#define LOBBY_H

#include "spreadsheet.h"
#include <string>
#include <map>
#include <sys/socket.h>
#include <pthread.h>
class Lobby {
  private:
    bool running;
    int port;
    std::map<std::string, Spreadsheet> spreadsheets;
    std::vector<std::string> sheet_list;

    static void* ListenForClients(void* ptr);
    static void* Handshake(void* ptr);
    static void* PingLoop(void* ptr);
    static std::string BuildConnectAccepted(Lobby* lobby);
    bool CheckForNewClient();
    void InitNewClient(int id);
    std::string ParseSheetList();
    static void Send(int socket_id, std::string message);
    void UpdateSheetList(std::string name);
    void OpenSpreadsheet(std::string filename);
    std::vector<std::string> GetSheetList();
  public:
    Lobby(int port);
    void Start();
    void Shutdown();

};

#endif   
