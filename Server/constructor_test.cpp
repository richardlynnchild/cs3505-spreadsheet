#include <iostream>
#include <string>
#include <vector>
#include <fstream>
using namespace std;
int main()
{
  std::ifstream in_file;
  std::string file_name = "sheet_list.txt";
  std::vector<std::string> sheet_list;

  in_file.open(file_name.c_str());

  std::string spreadsheet_name;

  while(!in_file.eof())
  {
    getline(in_file, spreadsheet_name);

    sheet_list.push_back(spreadsheet_name);
  }

  in_file.close();

  for(int i = 0; i < sheet_list.size(); i++)
  {
    cout<< "sheet_list " << i << sheet_list[i] << endl;
  }
}
