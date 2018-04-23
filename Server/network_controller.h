#ifndef NETWORK_CONTROLLER
#define NETWORK_CONTROLLER

#include <string>
#include "lobby.h"

#define PORT 2112

struct socket_data {
  int client_socket;
  Lobby* ptr_lobby;
};

class NetworkController
{

private:	

    static void* Handshake(void* ptr);
	static std::string GetSpreadsheetChoice(int socket_id, Lobby* lobby_ptr);
    static std::string GetMessage(char* msg_buf, int &buf_end);
    static void CleanBuffer(char* buffer, int msg_start, int msg_end);
    static void ClearBuffer(char* buffer, int buf_size);
	static void SetSocketTimeout(int socket_id);
	static bool TestSocket(int socket_id);
	static void SendDisconnect(int socket_id);
	static void ReplyPing(int socket_id);
	static bool IsDisconnect(std::string msg);
	static bool IsPing(std::string msg);

public:
    
    static void* ListenForClients(void* ptr);
	static void* ClientCommunicate(void* ptr);
};

#endif
