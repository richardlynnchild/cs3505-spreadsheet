#include "lobby.h"
#include <sstream>
#include <sys/socket.h>
#include <queue>
#include <stdio.h>
#include <stdlib.h>
#include <netinet/in.h>
#include <string.h>
#include <iostream>

class Interface
{
private:
    std::queue incoming;
    std::queue outgoing;
    int server_socket_fd;
    __socket_type socket_type;
    int socket_domain;
    int level;
    int options_name;
    int options_value;
    sockaddr_in  * socket_address_info;
    void AcceptConnection();

public:
    void Receive();
    void Send();
    void ConnectClient(int port, unsigned addr);
    void ConnectServer(int port, unsigned addr);
    Interface();
    Interface(__socket_type sockType, int sockDomain, int lvl, int optName, int optVal);

};
