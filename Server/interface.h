#ifndef INTERFACE_H
#define INTERFACE_H

#include <string>
#include <queue>
#include <pthread.h>

#define LOBBY 0
#define CLIENT 1

class Interface
{
private:

	// Locks for queues
    pthread_mutex_t in_msg_mutex;
    pthread_mutex_t out_msg_mutex;

	pthread_t client_thread;
    bool thread_active, thread_joined;
    std::string spreadsheet_name;
    std::queue<std::string> inbound_messages;
    std::queue<std::string> outbound_messages;

    //interface and client networking socket
    int client_socket_id;

public:

    Interface(int socket_id, std::string sprd_name);

    void PushMessage(int access_id, std::string message);    
    std::string PullMessage(int access_id);
    std::string GetMessage();
    std::string GetSprdName();
    int GetClientSocketID();
    bool IsActive();
	void MarkThreadClosed();
    bool StartClientThread();
    void StopClientThread();
};

#endif
