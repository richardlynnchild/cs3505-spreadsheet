#include <sstream>
#include <queue>


class Interface
{
private:
    //incoming and outgoing message buffers.
    static const int buf_size = 1024;
    char message_buffer[buf_size];

    std::stringstream messages;

    std::string spreadsheet_name;

    std::queue outbound_messages;

    //interface and client networking sockets
    int interfaceSocket_id, clientSocket_id;

    //helper methods
    void ClearBuffer(char buffer[]);
    void Receive();

public:
    //public interface methods
    void Send(std::string);

    std::string GetMessage();

    std::string GetSprdName();

    Interface(int socket_id);
}
