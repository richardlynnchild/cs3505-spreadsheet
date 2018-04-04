#include "spreadsheet.h"
#include <string>
#include <map>
#include <sys/socket.h>
#include <pthread.h>
class Lobby {
  private:
    std::map<std::string, Spreadsheet> spreadsheets;
    std::vector<std::string> sheet_list;
    void GetSheetList();
    std::string ParseSheetList();
    void Send(std::string message, int socket_id);
    void UpdateSheetList(std::string name);
    void OpenSpreadsheet(std::string filename);
  public:
    Lobby(int port);
    void Start();
    void Shutdown();

};

    
