#include "spreadsheet.h"

Spreadsheet::Spreadsheet(std::string sheet_name)
{
  name = sheet_name;
}
  
/*
 * Return name of this Spreadsheet
 */
std::string Spreadsheet::GetName()
{
  return name;
}

/*
 * Return contents of specified cell
 */
std::string Spreadsheet::GetState(std::string cell_name)
{
  return spreadsheet_state[cell_name];
}

/*
 * Return "full_state" message for this Spreadsheet
 */
std::string Spreadsheet::GetFullState(){
  std::string msg = "full_state ";
  std::map<std::string,std::string>::iterator it = spreadsheet_state.begin();
  for(; it != spreadsheet_state.end(); ++it){
    msg += it->first;
    msg += ":";
    msg += it->second;
    msg += "\n";
  }
  char end = (char) 3;
  msg += end;
  return msg;
}

/*
 * Edit the cell, push the edit onto the undo stack, push the new
 * contents onto the revert stack for the specified cell.
 */
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
