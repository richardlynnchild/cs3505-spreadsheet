#include "lobby.h"
#include <iostream>
#include <string>

int main(int argc, char* argv[])
{
  std::string input;
  
  std::cout << "Starting test" << std::endl;
  Lobby lobby;
  std::cout << "Created Lobby" << std::endl;
  lobby.Start();
  std::cout << "Server started" << std::endl;
  while (true)
  {
    std::cin >> input;
    if (input == "exit" || !lobby.IsRunning())
    {
      lobby.Shutdown();
      break;
    }
  }
}
