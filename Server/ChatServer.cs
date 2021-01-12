using Logger;
using MessageSender.Commands;
using MessageSender.Interfaces;
using Server.Interfaces;
using System;
using System.Linq;

namespace Server
{
    public class ChatServer : IDisposable
    {
        private IServer _server = null;
        ILogger _logger = null;

        public ChatServer(IServer server, ILogger logger) 
        {
            _server = server;
            _logger = logger;

            _server.OnClientConnection -= _server_OnClientConnection;
            _server.OnClientConnection += _server_OnClientConnection;

            _server.OnException -= _server_OnException;
            _server.OnException += _server_OnException;

            _server.OnGetMessage -= _server_OnGetMessage;
            _server.OnGetMessage += _server_OnGetMessage;
        }

        private void _server_OnGetMessage(object sender, string e)
        {
            if(e == Commands.GetLogCommand())
            {
                var log = _logger.ReadLog();
                _server.SendMessage(sender as IMessageSender, log);
            }
            else
            {
                _logger.Log(e);
                var anotherClients = _server.Clients.Where(c => !c.Equals(sender as IMessageSender)).ToList();
                if(anotherClients != null && anotherClients.Count != 0)
                {
                    foreach (var client in anotherClients)
                        _server.SendMessage(client, e);
                }
            }
        }

        private void _server_OnException(object sender, Exception e)
        {
            //throw new NotImplementedException();
        }

        private void _server_OnClientConnection(object sender, IMessageSender e)
        {
            //throw new NotImplementedException();
        }

        public void Listen(IServerConnection connection)
        {
            _server.Listen(connection);
        }

        public void Stop()
        {
            _server?.Close();
        }

        public void Dispose()
        {
            _logger = null;

            if (_server is null)
                return;

            _server.OnClientConnection -= _server_OnClientConnection;
            _server.OnException -= _server_OnException;
            _server.Dispose();
            _server = null;
        }
    }
}
