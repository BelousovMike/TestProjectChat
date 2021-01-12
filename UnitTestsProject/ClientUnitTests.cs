using ChatClient;
using MessageSender.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System.Net;

namespace UnitTestsProject
{
    [TestFixture]
    public class ClientUnitTests
    {
        [Test]
        public void Test_Connect_GoodConnect()
        {
            // В NUnit есть методы инициализации, которые вызываются перед каждым 
            // вызовом теста [SetUp], и после каждого вызова теста [TearDown]
            // и то что соответствует  Arrange и общее для всех тестов, можно было вынести туда.
            // Но для удобочитаемости, оставлено здесь. Хоть это и дублирование кода в тестах, зато не 
            // приходиться бегать от метода к методу, чтобы понять, что откуда взялось

            // Arrange
            var stubSender = Substitute.For<IMessageSender>();
            var client = new ChatClient.Client(stubSender);
            stubSender.IsConnected.Returns(true);

            // Act
            client.Connect("127.0.0.1", 5050);

            // Assert
            Assert.IsTrue(client.IsConnected);
        }

        [Test]
        public void Test_Disconnect_GoodDisconnect()
        {
            var stubSender = Substitute.For<IMessageSender>();
            var client = new ChatClient.Client(stubSender);
            stubSender.IsConnected.Returns(false);

            client.Disconnect();

            Assert.IsFalse(client.IsConnected);
        }

        [Test]
        public void Test_Connect_ReceivedDisconnectFromIMessageSender()
        {
            var mockConnection = Substitute.For<IMessageSender>();
            var client = new ChatClient.Client(mockConnection);

            client.Disconnect();

            mockConnection.Received().Disconnect();
        }

        [Test]
        public void Test_Connect_ReceivedWaitMessage()
        {
            var mockConnection = Substitute.For<IMessageSender>();
            var client = new ChatClient.Client(mockConnection);

            client.Connect("127.0.0.1", 5050);

            mockConnection.Received().WaitMessage();
        }

        [Test]
        public void Test_Connect_ReceivedConnect()
        {
            var mockConnection = Substitute.For<IMessageSender>();
            var client = new ChatClient.Client(mockConnection);
            var port = 5050;
            var ipAddress = "127.0.0.1";

            client.Connect(ipAddress, port);

            mockConnection.Received().Connect(IPAddress.Parse(ipAddress), port);
        }

        [Test]
        public void Test_SendMessage_Received()
        {
            var mockConnection = Substitute.For<IMessageSender>();
            var client = new ChatClient.Client(mockConnection);
            var message = "Hello World";

            client.SendMessage(message);

            mockConnection.Received().SendMessage(message);
        }
    }
}
