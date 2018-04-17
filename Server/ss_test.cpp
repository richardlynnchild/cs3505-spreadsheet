#include "spreadsheet.h"
#include <iostream>
int main(){
  Spreadsheet s("test");
  s.EditSheet("A6","3");
  s.EditSheet("A9","=A6/2");
  std::string actual = s.GetFullState();
  std::string expected = "full_state A6:3\nA9:=A6/2\n";
  char end = (char) 3;
  expected += end;
  std::cout << "Testing Spreadsheet::GetFullState()" << std::endl;
  std::cout << "Expected: " <<  expected << std::endl;
  std::cout << "Actual: " << actual << std::endl;

}
