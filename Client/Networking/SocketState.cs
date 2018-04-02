using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Networking {

    /// <summary>
    /// Object to hold a socket and "buffer" state through network connection calls
    /// </summary>
    public class SocketState {
        public Socket sock;
        public byte[] buffer = new byte[1024];
        public StringBuilder builder = new StringBuilder();
        public NetworkAction callMe;
        public int ID;

        public SocketState(Socket s) {
            sock = s;
        }

    }
}
