using System;

namespace Server.Exceptions
{
    public class ServerConnectException : Exception
    {
        public ServerConnectException() : base()
        {
        }

        public ServerConnectException(string message) : base(message)
        {
        }

        public ServerConnectException(Exception innerException) : base("Соединение: ошибка подключения к серверу", innerException)
        {
        }
    }
}
