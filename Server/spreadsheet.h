#ifndef SPREADSHEET_H
#define SPREADSHEET_H

#include <string>
#include <map>
#include <vector>
#include <pthread.h>
#include <stack>

class Spreadsheet
{
 private:
  
  pthread_rwlock_t msg_lock;
  std::vector<std::string> msgs;

  std::map < std::string, std::string > spreadsheet_state;
  std::map<int,int> clients;
  std::map<std::string, std::stack<std::string> > revert_stacks;
  std::stack<std::pair <std::string,std::string> > undo_stack;
  std::string name;
//  std::vector<std::string> SplitString(std::string input, char delim);
  
  void AddCell(std::string cell_name, std::string contents);
  void EditSheet(std::string cell_name, std::string contents);
  std::pair<std::string, std::string> Undo();
  std::string Revert(std::string cell_name);

  std::vector<std::string> GetEditMsg(std::string input);
  std::vector<std::string> SplitString(std::string str, char delim);

 public:
  Spreadsheet(std::string name);
  Spreadsheet();
  Spreadsheet(std::string name, std::string filename);
  bool ReadSpreadsheet(std::string filename);
  bool WriteSpreadsheet(std::string filename);
  std::string GetName();
  std::string GetState(std::string cell_name);
  std::string GetFullState(int id);
  void HandleMessage(std::string message, int id);
  int GetMessages(int id, char* buf, int size);
};

#endif
