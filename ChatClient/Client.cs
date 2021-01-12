using MessageSender.Interfaces;
using System;
using System.Net;

namespace ChatClient
{
    public class Client : IDisposable
    {
        public event EventHandler<string> ReadMessage;
        public event EventHandler Connected;
        public event EventHandler Disconnected;
        public event EventHandler<Exception> OnException;

        private IMessageSender _sender = null;
        public bool IsConnected => _sender == null ? false : _sender.IsConnected;

        public Client(IMessageSender sender)
        {
            _sender = sender;
        }

        public void Connect(string ipAddress, int port)
        {
            SubscribeEvents();
            try
            {
                _sender.Connect(IPAddress.Parse(ipAddress), port);
                _sender.WaitMessage();
            }
            catch(Exception ex)
            {
                OnException?.Invoke(this, ex);
            } 
        }

        public void Disconnect()
        {
            _sender.Disconnect();
            UnsubscribeEvents();
        }

        public void SendMessage(string message)
        {
            _sender.SendMessage(message);
        }

        public void Dispose()
        {
            _sender = null;
        }

        private void SubscribeEvents()
        {
            _sender.OnRead -= _connection_OnRead;
            _sender.OnRead += _connection_OnRead;

            _sender.OnConnected -= _connection_OnConnected;
            _sender.OnConnected += _connection_OnConnected;

            _sender.OnDisconnected -= _connection_OnDisconnected;
            _sender.OnDisconnected += _connection_OnDisconnected;

            _sender.OnException -= _connection_OnException;
            _sender.OnException += _connection_OnException;
        }

        private void UnsubscribeEvents()
        {
            _sender.OnRead -= _connection_OnRead;
            _sender.OnConnected -= _connection_OnConnected;
            _sender.OnDisconnected -= _connection_OnDisconnected;
            _sender.OnException -= _connection_OnException;
        }

        private void _connection_OnException(object sender, Exception exception)
        {
            OnException?.Invoke(sender, exception);
        }

        private void _connection_OnDisconnected(object sender, EventArgs e)
        {
            Disconnected?.Invoke(sender, e);
        }

        private void _connection_OnConnected(object sender, EventArgs e)
        {
            Connected?.Invoke(sender, e);
        }

        private void _connection_OnRead(object sender, string e)
        {
            ReadMessage?.Invoke(sender, e);
        }
    }
}
