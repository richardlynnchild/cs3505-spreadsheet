#include <vector>
#include <string>
#include <sys/socket.h>
#include <stdlib.h>
#include <netinet/in.h>
#include "lobby.h"
//#include "interface_temp.h"

#ifndef NETWORK_CONTROLLER
#define NETWORK_CONTROLLER

struct socket_data {
  int client_socket;
  Lobby* ptr_lobby;
};

class NetworkController
{
private:
    static int CreateListenerSocket();
    static int CreateListenerSocket(__socket_type sockType, int sockDomain, int lvl, int optName, int optVal, int _port, unsigned _address);
    static void Send(std::string message);
    static int Receive(int client_socket_id, char* msg_buf, int buf_size);
    static std::string GetMessage(char* msg_buf, int buf_end);
    static void CleanBuffer(char* buffer, int msg_start, int msg_end);
    static void ClearBuffer(char* buffer, int buf_size);
public:
    
    static void* ListenForClients(void* ptr);
    static void* Handshake(void* ptr);
   // std::vector<Interface>;

};

#endif
