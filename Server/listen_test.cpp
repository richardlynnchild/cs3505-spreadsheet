#include "lobby.h"
#include <iostream>
#include <string>

int main(int argc, char* argv[])
{
  std::string input;
  int port = 2112;
  
  Lobby lobby(2112);
  lobby.Start();
  std::cout << "Server started" << std::endl;
  while (true)
  {
    std::cin >> input;
    if (input == "exit")
    {
      lobby.Shutdown();
      break;
    }
  }
}
