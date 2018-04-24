#include "interface.h"
#include <iostream>
#include <vector>
#include <sstream>
#include <fstream>
#include <string>
#include <cstring>
#include <bits/stdc++.h>
#include <pthread.h>


bool FileContains(std::string filename, std::string query)
{
  std::ifstream file;
  std::string line;

  file.open(filename.c_str());

  if ( ! file)
  {
    std::cout << "Failed to open file in FileContains" << std::endl;
    return false;
  }

  if (file.is_open())
  {
    while (getline (file, line))
    {
      if (line == query)
        return true;
    }
  }
  return false;
}

//Splits a string into two parts on the first space character it encounters
std::vector<std::string> Spreadsheet::SplitString(std::string input, char delim)
{
  std::string section = "";
  std::vector<std::string> split_sections;
  int index = 0;

  while (index < input.length())
  {
    if (input[index] != delim)
    {
      section += input[index];
    }
    else
    {
      split_sections.push_back(section);
      split_sections.push_back(input.substr(index+1));
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
  if (pthread_rwlock_init(&msg_lock, NULL))
    std::cout << "RWLOCK FAILURE" << std::endl;
  name = sheet_name;
  ReadSpreadsheet(sheet_name);
}

Spreadsheet::~Spreadsheet()
{
  pthread_rwlock_destroy(&msg_lock);
}

/*
 * Default constructor
 */
Spreadsheet::Spreadsheet()
{
  name = "";
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

  filename = filename + ".sprd";

  file.open(filename.c_str());

  if ( ! file)
  {
    std::cout << "Created new spreadsheet" << std::endl;
    std::cout << "Filename: " << filename << std::endl;
    return false;
  }

  if (file.is_open())
  {
    while (getline(file, line))
    {
      if (line == "***FILE_END***")
        break;
      cell_name_val = SplitString(line, ' ');

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

  std::string full_filename = filename + ".sprd";

  file.open(full_filename.c_str());

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

  //check to see if the file exits in the list of spreadsheets, if not add it.
  if ( ! FileContains("sheet_list.txt", filename))
  {
    std::ofstream sheet_list;

    sheet_list.open("sheet_list.txt", std::ios_base::app);

    if ( ! sheet_list)
    {
      std::cout << "Failed to open the sheet list" << std::endl;
      return false;
    }

    if (sheet_list.is_open())
    {
      sheet_list << filename;
      sheet_list << '\n';
    }
  }

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
std::string Spreadsheet::GetFullState(int id){
  std::string msg = "full_state ";
  pthread_rwlock_rdlock(&msg_lock);
  std::map<std::string,std::string>::iterator it = spreadsheet_state.begin();
  for(; it != spreadsheet_state.end(); ++it){
    msg += it->first;
    msg += ":";
    msg += it->second;
    msg += "\n";
  }
  char end = (char) 3;
  msg += end;
  clients[id] = msgs.size();
  pthread_rwlock_unlock(&msg_lock);
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
  if(undo_stack.size() == 0){
    return std::make_pair("NULL","NULL");
  }
  std::pair<std::string, std::string> ret = undo_stack.top();
  undo_stack.pop();
  return ret;
}
  
std::string Spreadsheet::Revert(std::string cell_name)
{
  if(revert_stacks[cell_name].size()<1){
    return "NULL";
  }  
  undo_stack.push(std::pair<std::string,std::string>(cell_name,spreadsheet_state[cell_name]));
  std::string ret = revert_stacks[cell_name].top();
  revert_stacks[cell_name].pop();
  spreadsheet_state[cell_name] = ret;
  return ret;
}

int Spreadsheet::GetMessages(int id, char* buf, int size)
{
  int msg_i = clients[id];
  int buf_i = 0;
  std::string message = "";
  std::string str = "";
  pthread_rwlock_rdlock(&msg_lock);
  while (msg_i < msgs.size())
  {
    str = msgs[msg_i];
    buf_i += str.length();
    if (buf_i < size)
    {
      message += str;
      msg_i++;
    }
    else
    {
      buf_i -=str.length();
      break;
    }
  }
  pthread_rwlock_unlock(&msg_lock);
  std::strcpy(buf, message.c_str());
  clients[id] = msg_i;
  return buf_i;
} 

void Spreadsheet::HandleMessage(std::string message, int id){
  //Split the message and get the command
  char delim = ' ';
  char end = (char)3;
  std::vector<std::string> tokens = SplitString(message, delim);
  if (tokens.empty())
    return;
  std::string command = tokens[0];

  if(command == "edit"){
    std::vector<std::string> cell = GetEditMsg(message);
    if (cell.empty())
      return;
    pthread_rwlock_wrlock(&msg_lock);
    EditSheet(cell[0],cell[1]);
    message = "change ";
    message += cell[0];
    message += ":";
    message += cell[1];
    message += end;
    msgs.push_back(message);
    pthread_rwlock_unlock(&msg_lock);
  }
  else if(command == "undo"){
    pthread_rwlock_wrlock(&msg_lock);
    std::pair<std::string,std::string> cell = Undo();
    if(cell.first != "NULL")
    {
      message = "change ";
      message += cell.first;
      message += ":";
      message += cell.second;
      message += end;
      msgs.push_back(message);
    }
    pthread_rwlock_unlock(&msg_lock);
  }
  else if(command == "revert"){
    if (tokens.size() < 2)
      return;
    pthread_rwlock_wrlock(&msg_lock);
    std::string revertedcell = Revert(tokens[1]);
    if(revertedcell != "NULL")
    {
      message = "change ";
      message += tokens[1];
      message += ":";
      message += revertedcell;
      message += end;
      msgs.push_back(message);
    }
    pthread_rwlock_unlock(&msg_lock);
  }
  else if(command == "focus"){
    message += ":";
    std::stringstream ss;
    ss << id;
    message += ss.str();
    message += end;
    pthread_rwlock_wrlock(&msg_lock);
    msgs.push_back(message);
    pthread_rwlock_unlock(&msg_lock); 
  }
  else if(command == "unfocus"){
    std::stringstream ss;
    ss << id;
    message += ss.str();
    message += end;
    pthread_rwlock_wrlock(&msg_lock);
    msgs.push_back(message);
    pthread_rwlock_unlock(&msg_lock); 
  }

}

std::vector<std::string> Spreadsheet::GetEditMsg(std::string input)
{
  //remove "edit ""
  std::string trimmed_input = input.substr(5);
  std::vector<std::string> cellAndVal = SplitString(trimmed_input, ':');
  return cellAndVal;
}

