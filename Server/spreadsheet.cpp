#include "spreadsheet.h"
#include <iostream>
#include <vector>
#include <fstream>
#include <bits/stdc++.h>

/*
std::vector<std::string> SplitString(std::string input, char delimiter)
{
  int index = 0;
  std::string section = "";
  std::vector<std::string> split_sections;
  std::cout << "input into splitstring: "<< input << std::endl;
  while (index < input.length() - 1)
  {
    if (input[index] != delimiter[0])
    {
        section += input[index];
    }

    else
    {
        split_sections.push_back(section);
        section = "";
    }
    index++;

  }
  section += input[index];
  split_sections.push_back(section);
  return split_sections;
}
*/

//Splits a string into two parts on the first space character it encounters
std::vector<std::string> SplitString(std::string input)
{
  std::string section = "";
  std::vector<std::string> split_sections;
  int index = 0;

  while (true)
  {
    if (input[index] != ' ')
    {
      section += input[index];
    }
    else
    {
      split_sections.push_back(section);
      split_sections.push_back(input.substr(index));
      break;
    }
    index++;
  }

  return split_sections;
}

/*
 * Constructor with spreadsheet name
 */
Spreadsheet::Spreadsheet(std::string sheet_name)
{
  name = sheet_name;
}

/*
 * Default constructor
 */
Spreadsheet::Spreadsheet()
{
  name = "";
}

//Constructor with filename to read spreadsheet state from.
Spreadsheet::Spreadsheet(std::string sheet_name, std::string filename)
{
  name = sheet_name;
  ReadSpreadsheet(filename);
}

//Adds a cell to spreadsheet state, bypasses additions to revert and undo stack. Used for loading a spreadhseet from a file.
void Spreadsheet::AddCell(std::string cell_name, std::string contents)
{
    if(spreadsheet_state.count(cell_name)<1)
      spreadsheet_state.insert(std::pair<std::string,std::string>(cell_name,""));

    spreadsheet_state[cell_name] = contents;
}

//Reads a spreadsheet from a text file.
bool Spreadsheet::ReadSpreadsheet(std::string filename)
{
  std::string line;
  std::ifstream file;
  std::vector<std::string> cell_name_val;

  char filename_char_array[filename.length() +1];
  strcpy(filename_char_array, filename.c_str());

  file.open(filename_char_array);

  if ( ! file)
  {
    std::cout << "File could not be opened. Does it exit, or ts it currently being written too or read from?" << std::endl;
    return false;
  }

  if (file.is_open())
  {
    while (getline(file, line))
    {
      if (line == "***FILE_END***")
        break;
      cell_name_val = SplitString(line);

      AddCell(cell_name_val[0], cell_name_val[1]);
    }
  }
  file.close();
  return true;

}

//Writes the state of the spreadsheet to a text file. Terminated with ***FILE_END***
bool Spreadsheet::WriteSpreadsheet(std::string filename)
{
  std::ofstream file;

  char filename_char_array[filename.length() +1];
  strcpy(filename_char_array, filename.c_str());

  file.open(filename_char_array);

  if ( ! file)
  {
    std::cout << "Failed to open the file" << std::endl;
    return false;
  }

  if (file.is_open())
  {
    std::string msg;
    std::map<std::string,std::string>::iterator it = spreadsheet_state.begin();
    for(; it != spreadsheet_state.end(); ++it)
    {
      msg += it->first;
      msg += " ";
      msg += it->second;
      msg += "\n";

      file << msg;
      msg = "";
    }
  }
  file << "***FILE_END***";

  file.close();
  return true;
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
  if(spreadsheet_state.count(cell_name)<1){
    spreadsheet_state.insert(std::pair<std::string,std::string>(cell_name,""));
  }
  if(revert_stacks.count(cell_name)<1){
    std::stack<std::string> stack;
    revert_stacks.insert(std::pair< std::string,std::stack<std::string> >(cell_name,stack));
  } 
  undo_stack.push(std::pair<std::string,std::string>(cell_name,spreadsheet_state[cell_name]));
  revert_stacks[cell_name].push(spreadsheet_state[cell_name]);
  spreadsheet_state[cell_name] = contents;
}
  
std::pair<std::string, std::string> Spreadsheet::Undo()
{
  std::pair<std::string, std::string> ret = undo_stack.top();
  undo_stack.pop();
  return ret;
}
  
std::string Spreadsheet::Revert(std::string cell_name)
{
  undo_stack.push(std::pair<std::string,std::string>(cell_name,spreadsheet_state[cell_name]));
  std::string ret = revert_stacks[cell_name].top();
  revert_stacks[cell_name].pop();
  spreadsheet_state[cell_name] = ret;
  return ret;
}
