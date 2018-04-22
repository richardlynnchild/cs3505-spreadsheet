#include "lobby.h"
#include <iostream>
#include "interface.h"
#include <stdio.h>
#include <sys/socket.h>
#include <stdlib.h>
#include <netinet/in.h>
#include <string.h>
#include <arpa/inet.h>
#include <sstream>
#include <algorithm>
#include <vector>

using namespace std;

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
