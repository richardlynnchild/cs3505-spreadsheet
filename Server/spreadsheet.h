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
  std::vector<std::string> SplitString(std::string input, char delim);
  
 public:
  Spreadsheet(std::string name);
  Spreadsheet();
  Spreadsheet(std::string name, std::string filename);
  void AddCell(std::string cell_name, std::string contents);
  bool ReadSpreadsheet(std::string filename);
  bool WriteSpreadsheet(std::string filename);
  void EditSheet(std::string cell_name, std::string contents);
  std::pair<std::string, std::string> Undo();
  std::string Revert(std::string cell_name);
  std::string GetName();
  std::string GetState(std::string cell_name);
  std::string GetFullState();
};

#endif
