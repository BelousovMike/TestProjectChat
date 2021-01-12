using MessageSender.Interfaces;
using System;

namespace Server.Interfaces
{
    public interface IServerConnection : IDisposable
    {
        void Start();
        ISenderConnection WaitClientConnection();
        void Stop();
    }
}
