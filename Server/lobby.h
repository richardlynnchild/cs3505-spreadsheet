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
    std::string BuildConnectAccept();
    bool CheckForNewClient();
    void InitNewClient(int id);
    void GetSheetList();
    std::string ParseSheetList();
    void Send(int socket_id, std::string message);
    void UpdateSheetList(std::string name);
    void OpenSpreadsheet(std::string filename);
<<<<<<< HEAD
    std::string BuildConnectAccepted();
    void* Handshake(void* ptr);
    void* ListenForClients(void* ptr);
=======
 
>>>>>>> 39f00cbca9b8d9e60e151a312ac423b57aea456d
  public:
    Lobby(int port);
    void Start();
    void Shutdown();

};

#endif   
