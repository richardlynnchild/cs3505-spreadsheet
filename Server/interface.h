#ifndef INTERFACE_H
#define INTERFACE_H


#include <stdio.h>
#include <string.h>
#include <sys/socket.h>
#include <stdlib.h>
#include <netinet/in.h>
#include <sstream>
#include <queue>
#include <pthread.h>


class Interface
{
private:

    pthread_mutex_t in_msg_mutex;
    pthread_mutex_t out_msg_mutex;
    //incoming and outgoing message buffers.
    static const int buf_size = 1024;
    char message_buffer[buf_size];

    std::string messages;

    std::string spreadsheet_name;

    std::queue<std::string> outbound_messages;

    //interface and client networking sockets
    int interfaceSocket_id, clientSocket_id;

    //helper methods
    void ClearBuffer(char buffer[]);
    void Receive();
    std::string GetLine(std::string &buf);

public:
    //public interface methods
    void Send(std::string);

    std::string GetMessage();

    std::string GetSprdName();

    int GetClientID();

    Interface(int socket_id, std::string sprd_name);

    void StartClientThread();
    void StopClientThread();
};

#endif
