using System;

namespace MessageSender.Exceptions
{
    public class MessageSenderReadException : Exception
    {
        public MessageSenderReadException() : base()
        {
        }

        public MessageSenderReadException(string message) : base(message)
        {
        }

        public MessageSenderReadException(Exception innerException) : base("Соединение: ошибка чтения ", innerException)
        {
        }
    }
}
