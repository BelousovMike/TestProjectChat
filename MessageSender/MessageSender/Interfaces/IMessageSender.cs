using System;
using System.Net;

namespace MessageSender.Interfaces
{

    public interface IMessageSender
    {
        event EventHandler<string> OnRead;
        event EventHandler OnConnected;
        event EventHandler OnDisconnected;
        event EventHandler<Exception> OnException;

        bool IsConnected { get; }

        void Connect(IPAddress ipAddress, int port);

        void SendMessage(string message);

        void WaitMessage();

        void Disconnect();
    }
}
