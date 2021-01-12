using MessageSender;
using MessageSender.Interfaces;
using Server.Interfaces;
using System.Net.Sockets;

namespace Server
{
    public class TcpListenerAdapter : IServerConnection
    { 
        private TcpListener _listener = null;
        
        public TcpListenerAdapter(TcpListener listener)
        {
            _listener = listener;
        }

        public void Dispose()
        {
            _listener = null;
        }

        public void Start()
        {
            _listener.Start();
        }

        public void Stop()
        {
            _listener.Stop();
        }

        public ISenderConnection WaitClientConnection()
        {
            return new TcpClientAdapter(_listener.AcceptTcpClient());
        }
    }
}
