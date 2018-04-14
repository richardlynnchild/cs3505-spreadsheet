#include <vector>
#include <sys/socket.h>
#include <stdlib.h>
#include <netinet/in.h>
#include "lobby.h"
//#include "interface_temp.h"

#ifndef NETWORK_CONTROLLER
#define NETWORK_CONTROLLER

struct socket_data {
  int* ptr_client;
  Lobby* ptr_lobby;
};

class NetworkController
{
private:
    static int CreateListenerSocket();
    static int CreateListenerSocket(__socket_type sockType, int sockDomain, int lvl, int optName, int optVal, int _port, unsigned _address);

public:
    
    static void* ListenForClients(void* ptr);
    static void* Handshake(void* ptr);
   // std::vector<Interface>;

};

#endif
