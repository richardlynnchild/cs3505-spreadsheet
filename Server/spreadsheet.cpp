#include "spreadsheet.h"
using namespace std;

Spreadsheet::Spreadsheet(std::string sheet_name)
{
  name = sheet_name;
}
  
std::string Spreadsheet::GetName()
{
  return name;
}
void Spreadsheet::EditSheet(std::string cell_name, std::string contents)
{
  spreadsheet_state[cell_name] = contents;
  revert_stacks[cell_name].push(contents);
  undo_stack.push(std::make_pair(cell_name, contents));
}
  
std::pair<std::string, std::string> Spreadsheet::Undo()
{
  std::pair<std::string, std::string> ret = undo_stack.top();
  undo_stack.pop();
  return ret;
}
  
std::string Spreadsheet::Revert(std::string cell_name)
{
  std::string ret = revert_stacks[cell_name].top();
  revert_stacks[cell_name].pop();
  return (ret);
}
