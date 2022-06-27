using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class ClientData
    {
        public Socket clientsocket;
        public Thread clientThread;
        public string id;
        public ClientData()
        {
            id = Guid.NewGuid().ToString();
            clientThread = new Thread(Server.DataIn);
            clientThread.Start(clientsocket);
        }
        public ClientData(Socket _clientSocked)
        {
            clientsocket = _clientSocked;
            id = Guid.NewGuid().ToString();
            clientThread = new Thread(Server.DataIn);
            clientThread.Start(clientsocket);
        }
    }
}
