#include <stdio.h>
#include <string.h>
#include <sys/socket.h>
#include <stdlib.h>
#include <netinet/in.h>
#include <sstream>
#include <queue>
#include "interface.h"
#include <pthread.h>

Interface::Interface(int socket_id, std::string sprd_name)
{
    interfaceSocket_id = socket_id;
    spreadsheet_name = sprd_name;
    in_msg_mutex = PTHREAD_MUTEX_INITIALIZER; 
    out_msg_mutex = PTHREAD_MUTEX_INITIALIZER; 
}

//Sends a specified message to the client.
void Interface::Send(std::string message)
{
    //convert string message to char[].
    int message_length = message.length() + 1;
    char msg_char[message_length];
    strcpy(msg_char, message.c_str());

    send(clientSocket_id, msg_char, message_length, 0);
}

//Clears the memory associated with the given char[].
void Interface::ClearBuffer(char buffer[])
{
    char * array_start = buffer;
    char * array_end = array_start + buf_size;
    std::fill(array_start, array_end, 0);
}

//Reads from the incoming buffer and returns any messages sent from the client.
void Interface::Receive()
{
    int bytes_recieved = recv(clientSocket_id, message_buffer, buf_size, 0);
    std::string msg(message_buffer);
    ClearBuffer(message_buffer);
    messages += msg;
}

//
std::string Interface::GetMessage()
{
    //get any new data
    Receive();

    //loop through the recieved data and add completed messages to the outboudn queue.
    while (true)
    {
        std::string msg = GetLine(messages);
        if (msg == "***NO_COMPLETED_MESSAGES_FOUND***")
            break;
        outbound_messages.push(msg);
    }

    std::string return_msg = outbound_messages.front();
    outbound_messages.pop();

    return return_msg;
}

//helper method that returns a message and removes it from the messages string buffer, only if the message is complete ('\n' found).
std::string Interface::GetLine(std::string &buffer)
{
    std::string::size_type position = buffer.find('\n');
    if (position != std::string::npos)
    {
        std::string return_msg = buffer.substr(0, position);
        buffer.erase(0, position+1);
        return return_msg;
    }
    return "***NO_COMPLETED_MESSAGES_FOUND***";
}

//returns the filename of the associated spreadsheet.
std::string Interface::GetSprdName()
{
    return spreadsheet_name;
}

void Interface::StartClientThread()
{

}

void Interface::StopClientThread()
{

}
