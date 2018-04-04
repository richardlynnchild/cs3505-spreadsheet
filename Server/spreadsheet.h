#ifndef SPREADSHEET_H
#define SPREADSHEET_H

#include <sys/socket.h>
#include <pthread.h>
#include <string>
class Spreadsheet
{
 private:
  void OpenFile(std::string name);
  std::map<std::string, std::string> spreadsheet_state;
  std::vector<std::string> clients;
  std::map<std::string, std::queue<std::string>> revert_stacks;
  std::queue<std::pair<std::string,std::string>> undo_stack;
  void Send(std::string message);
  void AcceptNewClient();
 public:
  Spreadsheet(std::string name);

};

#endif SPREADSHEET_H
