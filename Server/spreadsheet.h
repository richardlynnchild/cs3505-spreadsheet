#ifndef SPREADSHEET_H
#define SPREADSHEET_H

#include <string>
#include <map>
#include <vector>
#include <stack>

class Spreadsheet
{
 private:
  std::map < std::string, std::string > spreadsheet_state;
  std::vector<std::string> clients;
  std::map<std::string, std::stack<std::string> > revert_stacks;
  std::stack<std::pair <std::string,std::string> > undo_stack;
  std::string name;
  
 public:
  Spreadsheet(std::string name);
  Spreadsheet();
  void EditSheet(std::string cell_name, std::string contents);
  std::pair<std::string, std::string> Undo();
  std::string Revert(std::string cell_name);
  std::string GetName();
  std::string GetState(std::string cell_name);
  std::string GetFullState();
};

#endif
