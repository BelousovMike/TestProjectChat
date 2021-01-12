using Logger;
using NSubstitute;
using NUnit.Framework;
using Server;
using Server.Interfaces;

namespace UnitTestsProject
{
    [TestFixture]
    public class ChatServerUnitTests
    {
        [Test]
        public void Test_Listen_ReceivedStartConnection()
        {
            var stubLogger = Substitute.For<ILogger>();
            var mockServer = Substitute.For<IServer>();
            var chatServer = new ChatServer(mockServer, stubLogger);
            var stubServerConnection = Substitute.For<IServerConnection>();

            chatServer.Listen(stubServerConnection);

            mockServer.Received().Listen(stubServerConnection);
        }

        [Test]
        public void Test_Listen_ReceivedStopConnection()
        {
            var stubLogger = Substitute.For<ILogger>();
            var mockServer = Substitute.For<IServer>();
            var chatServer = new ChatServer(mockServer, stubLogger);

            chatServer.Stop();

            mockServer.Received().Close();
        }
    }
}
