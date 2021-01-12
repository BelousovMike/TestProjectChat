using System;
using System.Net;
using System.Net.Sockets;

namespace MessageSender.Interfaces
{
  

    public interface ISenderConnection: IDisposable
    {
        bool IsConnected { get; }

        void Connect(IPAddress ipAddresses, int port);
        NetworkStream GetStream();
        void Disconnect();
    }
}
