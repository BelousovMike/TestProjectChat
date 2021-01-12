using MessageSender.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Server;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestsProject
{
    public class TestMainServer : MainServer
    {
        protected override void WaitingClientConnect()
        {
            
        }

        protected override void WaitMessage(IMessageSender sender)
        {
        }
    }

    [TestFixture]
    public class MainServerUnitTests
    {
        [Test]
        public void Test_StartServer_GoodStart()
        {
            var mainServer = new TestMainServer();
            var mockConnection = Substitute.For<IServerConnection>();

            mainServer.Listen(mockConnection);

            mockConnection.Received().Start();
        }

        [Test]
        public void Test_SendMessage()
        {
            var mockSender = Substitute.For<IMessageSender>();
            mockSender.IsConnected.Returns(true);
            var mainServer = new TestMainServer();
            var message = "Hello World";

            mainServer.SendMessage(mockSender, message);

            mockSender.Received().SendMessage(message);
        }

        [Test]
        public void Test_AddClient()
        {
            var stubConnection = Substitute.For<ISenderConnection>();

            stubConnection.IsConnected.Returns(true);
            var mainServer = new TestMainServer();

            mainServer.AddClient(stubConnection);
            var result = mainServer.Clients.Count();

            Assert.AreEqual(1, result);
        }

    }
}
