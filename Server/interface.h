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


public:
    void Receive();
    void Send();
    void Connect(sockaddr address);
    Interface( __socket_type sock_type, int socket_id, int socket_domain);

};
