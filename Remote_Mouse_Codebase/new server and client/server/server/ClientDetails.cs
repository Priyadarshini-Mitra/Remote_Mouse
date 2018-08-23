using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ClientDetails
    {
        public String ClientID;
        public String ClientName;
        public TcpClient clientSocket;
        public List<String> messages;

        public String status;

        public ClientDetails(String _ClientID, TcpClient _clientSocket)
        {
            ClientID = _ClientID;
            clientSocket = _clientSocket;
            messages = new List<string>();
            ClientName = "";
            status = "Asleep";
        }

        public void updateClientName(String _ClientName)
        {
            ClientName = _ClientName;
        }
    }
}
