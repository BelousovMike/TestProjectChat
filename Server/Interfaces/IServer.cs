using MessageSender.Interfaces;
using System;
using System.Collections.Generic;

namespace Server.Interfaces
{
    public interface IServer : IDisposable
    {
        event EventHandler<IMessageSender> OnClientConnection;
        event EventHandler<Exception> OnException;
        event EventHandler<string> OnGetMessage; 

        List<IMessageSender> Clients { get; }

        void Listen(IServerConnection connection);

        void SendMessage(IMessageSender sender, string message);

        void Close();
    }
}
