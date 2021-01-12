using Logger;
using MessageSender.Interfaces;
using Server.Exceptions;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("UnitTestsProject")]

namespace Server
{
    public class MainServer : IServer
    {
        public event EventHandler<IMessageSender> OnClientConnection;
        public event EventHandler<Exception> OnException;
        public event EventHandler<string> OnGetMessage;

        private IServerConnection _connection;
        private CancellationTokenSource _cancellationTokenSource = null;
        public List<IMessageSender> Clients { get; private set; } = null;

        public void Close()
        {
            _connection?.Stop();
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _connection = null;

            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            
            Clients = null;
        }

        public void Listen(IServerConnection connection)
        {
            _connection = connection;
            _connection.Start();

            WaitingClientConnect();
        }

        public void SendMessage(IMessageSender sender, string message)
        {
            if (sender.IsConnected)
                sender.SendMessage(message);
        }

        protected virtual void WaitingClientConnect()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            Task.Run(() =>
            {
                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        var client = _connection.WaitClientConnection();
                        AddClient(client);
                    }
                }
                catch (Exception ex)
                {
                    if (!token.IsCancellationRequested)
                        OnException?.Invoke(this, new ServerConnectException(ex));
                }
                finally
                {
                    Close();
                    Dispose();
                }
            });
        }

        protected internal void AddClient(ISenderConnection client)
        {
            if (Clients is null)
                Clients = new List<IMessageSender>();

            var messageSender = new MessageSender.MessageSender(client);
            

            messageSender.OnDisconnected -= MessageSender_OnDisconnected;
            messageSender.OnDisconnected += MessageSender_OnDisconnected;

            messageSender.OnRead -= MessageSender_OnRead;
            messageSender.OnRead += MessageSender_OnRead;

            Clients.Add(messageSender);
            WaitMessage(messageSender);

            OnClientConnection?.Invoke(this, messageSender);
        }

        protected virtual void WaitMessage(IMessageSender sender)
        {
            sender.WaitMessage();
        }

        private void MessageSender_OnRead(object sender, string e)
        {
            OnGetMessage?.Invoke(sender, e);
        }

        private void MessageSender_OnDisconnected(object sender, EventArgs e)
        {
            var client = sender as IMessageSender;
            client.OnDisconnected -= MessageSender_OnDisconnected;
            client.OnRead -= MessageSender_OnRead;
            if(Clients != null)
                Clients.Remove(client);
        }
    }
}
