using MessageSender.Interfaces;
using System.Net;
using System.Net.Sockets;

namespace MessageSender
{
    // Можно было подключение построить на классе Socket или UdpClient, или вообще использовать WCF

    // Класс для абстракции TcpClient
    // Паттерн: Адаптер

    public class TcpClientAdapter : ISenderConnection
    {
        private TcpClient _client;
        public TcpClientAdapter(TcpClient client)
        {
            _client = client;
        }

        public bool IsConnected => _client is null ? false : _client.Connected;

        public void Connect(IPAddress ipAddress, int port)
        {
            _client.Connect(ipAddress, port);
        }

        public NetworkStream GetStream()
        {
            return _client.GetStream();
        }

        public void Disconnect()
        {
            _client.Close();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
