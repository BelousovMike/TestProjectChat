using System;

namespace MessageSender.Exceptions
{
    public class MessageSenderSendException : Exception
    {
        public MessageSenderSendException() : base()
        {
        }

        public MessageSenderSendException(string message) : base(message)
        {
        }

        public MessageSenderSendException(Exception innerException) : base("Соединение: ошибка записи ", innerException)
        {
        }
    }
}
