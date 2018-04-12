#include <string>

class ClientController
{
  private:

   static void Receive(int client_socket_id, char& buffer[]);
   static void ParseMessages(char& buffer[]);
   static void ClearBuffer(char& buffer[]);
   static void Send(std::string message);
 
  public:

    void* ClientLoop(void* ptr);
};
