#include <string>
#include <cstring>
#include <iostream>
#include <queue>
#include <pthread.h>
#include <errno.h>
#include "interface.h"
#include "network_controller.h"
#include "spreadsheet.h"

Interface::Interface(int socket_id, std::string sprd_name)
{
    client_socket_id = socket_id;
    spreadsheet_name = sprd_name;
    thread_active = false;
	thread_joined = true;

	// Output is just for debugging
    int error;
    if (error = pthread_mutex_init(&in_msg_mutex, NULL) != 0)
	{
		std::cout << strerror(error) << std::endl;
	} 
    if (error = pthread_mutex_init(&out_msg_mutex, NULL) != 0)
	{
		std::cout << strerror(error) << std::endl;
	} 
}

// Needed for the client communication thread
int Interface::GetClientSocketID()
{
	return client_socket_id;
}

//returns the filename of the associated spreadsheet.
std::string Interface::GetSprdName()
{
    return spreadsheet_name;
}

Spreadsheet* Interface::GetSpreadPointer()
{
	return spread_ptr;
}

void Interface::SetSpreadPointer(Spreadsheet* ptr)
{
	spread_ptr = ptr;
}

void Interface::PushMessage(int access_id, std::string message)
{
	if (access_id == LOBBY)
	{
		pthread_mutex_lock(&out_msg_mutex);
		outbound_messages.push(message);
		pthread_mutex_unlock(&out_msg_mutex);
	}
	else if (access_id == CLIENT)
	{	
		pthread_mutex_lock(&in_msg_mutex);
		inbound_messages.push(message);
		pthread_mutex_unlock(&in_msg_mutex);
	}
}

std::string Interface::PullMessage(int access_id)
{
	std::string message = "";
	if (access_id == LOBBY)
	{
		pthread_mutex_lock(&in_msg_mutex);
		if (!inbound_messages.empty())
		{
			message = inbound_messages.front();
			inbound_messages.pop();
		}
		pthread_mutex_unlock(&in_msg_mutex);
	}
	else if (access_id == CLIENT)
	{	
		pthread_mutex_lock(&out_msg_mutex);
		if (!outbound_messages.empty())
		{
			message = outbound_messages.front();
			outbound_messages.pop();
		}
		pthread_mutex_unlock(&out_msg_mutex);
	}
	return message;
}

bool Interface::IsActive()
{
	return thread_active;
}

void Interface::MarkThreadClosed()
{
	thread_active = false;
}

bool Interface::StartClientThread()
{
	if (!thread_joined)
		return false;

	if (pthread_create(&client_thread, NULL, NetworkController::ClientCommunicate, this))
		std::cerr << "error creating thread for connected client" << std::endl;
    else
	{
		thread_active = true;
		thread_joined = false;
	}

	return thread_active;
}

void Interface::StopClientThread()
{
	if (!thread_joined)
	{
		if (thread_active)
		{
			std::string disconnect = "disconnect ";
			char end = (char)3;
			disconnect += end;
			PushMessage(LOBBY, disconnect);
		}
		if (pthread_join(client_thread, NULL))
			std::cout << "stopping client thread error" << std::endl;
		thread_joined = true;
	}
}
