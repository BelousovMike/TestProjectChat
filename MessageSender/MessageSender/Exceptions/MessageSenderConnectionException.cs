using System;

namespace MessageSender.Exceptions
{
    public class MessageSenderConnectionException : Exception
    {
        public MessageSenderConnectionException() : base()
        {
        }

        public MessageSenderConnectionException(string message) : base(message)
        {
        }

        public MessageSenderConnectionException(Exception innerException) : base("Соединение: ошибка подключения", innerException)
        {
        }
    }
}
