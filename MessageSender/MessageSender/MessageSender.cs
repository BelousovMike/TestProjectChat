using MessageSender.Exceptions;
using MessageSender.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace MessageSender
{
    public class MessageSender : IMessageSender
    {
        public event EventHandler<string> OnRead;
        public event EventHandler OnConnected;
        public event EventHandler OnDisconnected;
        public event EventHandler<Exception> OnException;

        private ISenderConnection _connection;
        private object writeCs = new object();
        private CancellationTokenSource _cancellationTokenSource;
        private StreamReader _streamReader = null;
        private StreamWriter _streamWriter = null;

        public bool IsConnected { get { return _connection is null ? false : _connection.IsConnected; } }

        public MessageSender(ISenderConnection connection)
        {
            _connection = connection;
        }

        public void Connect(IPAddress ipAddress, int port)
        {
            if (_connection != null && _connection.IsConnected)
                return;
            try
            {
                _connection.Connect(ipAddress, port);
                OnConnected?.Invoke(this, null);
            }
            catch (Exception ex)
            {
                DisposeClient();
                throw new MessageSenderConnectionException(ex);
            }
        }
        public void Disconnect()
        {
            DisposeClient();
            OnDisconnected.Invoke(this, null);
        }
        public void SendMessage(string message)
        {
            try
            {
                if (!IsConnected)
                    throw new MessageSenderSendException(new Exception("Cоединение отсутствует"));

                Task.Run(() =>
                {
                    try
                    {
                        lock (writeCs)
                        {
                            var networkStream = _connection.GetStream();
                            _streamWriter = new StreamWriter(networkStream);
                            _streamWriter.AutoFlush = true;
                            _streamWriter.WriteLine(message);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (_connection != null)
                            OnException?.Invoke(this, new MessageSenderSendException(ex));
                    }
                });
            }
            catch (Exception ex)
            {
                if (_connection != null)
                    OnException?.Invoke(this, new MessageSenderSendException(ex));
            }
        }
        public void Dispose()
        {
            DisposeClient();
        }
        public void WaitMessage()
        {
            if (_connection is null || _connection.IsConnected == false)
                return;

            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;
            var networkStream = _connection.GetStream();
            _streamReader = new StreamReader(networkStream);

            Task.Run(() =>
            {
                try
                {
                    while (_connection != null &&_connection.IsConnected && !token.IsCancellationRequested)
                    {
                        var read = _streamReader.ReadLine();

                        if (read != null)
                        {
                            OnRead?.Invoke(this, read);
                        }

                        Task.Delay(10).Wait();
                    }
                }
                catch (Exception ex)
                {
                    if (!token.IsCancellationRequested)
                        OnException?.Invoke(this, new MessageSenderReadException(ex));
                }
                finally
                {
                    DisposeClient();
                    OnDisconnected?.Invoke(this, null);
                }
            });
        }

        private void DisposeClient()
        {
            if (_connection is null)
                return;

            if (_cancellationTokenSource != null && _cancellationTokenSource.Token.CanBeCanceled)
                _cancellationTokenSource.Cancel();

            _streamReader?.Close();
            _streamReader?.Dispose();
            _streamReader = null;

            _streamWriter?.Close();
            _streamWriter?.Dispose();
            _streamWriter = null;

            _connection?.Dispose();
            _connection = null;
        }
    }
}
