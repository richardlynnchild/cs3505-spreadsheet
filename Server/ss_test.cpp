#include "spreadsheet.h"
#include <stdio.h>
#include <iostream>
int main(){
  Spreadsheet s("Test1");
  s.EditSheet("A1","1");
  s.EditSheet("A2","5");
  s.EditSheet("B1","3");
  s.EditSheet("B2","7");
  s.EditSheet("A1","17");
  s.EditSheet("A1", "99");
  s.EditSheet("A1", "0");
  std::pair<std::string,std::string> undo = s.Undo();
  std::cout << "Undo - " << undo.first << ":" << undo.second << std::endl;
  undo = s.Undo();
  std::cout << "Undo - " << undo.first << ":" << undo.second << std::endl;
  undo = s.Undo();
  std::cout << "Undo - " << undo.first << ":" << undo.second << std::endl;
  undo = s.Undo();
  std::cout << "Undo - " << undo.first << ":" << undo.second << std::endl;
}
