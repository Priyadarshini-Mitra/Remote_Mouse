using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FirstServer
{
    class ClientDetails
    {
        public String ClientID;
        public String ClientName;
        public TcpClient clientSocket;
        public List<String> messages;
        public List<String> Files;

        public ClientDetails(String _ClientID, TcpClient _clientSocket)
        {
            ClientID = _ClientID;
            clientSocket = _clientSocket;
            messages = new List<String>();
            Files = new List<String>();
            ClientName = "";
        }

        public void updateClientName(String _ClientName)
        {
            ClientName = _ClientName;
        }
    }
}
