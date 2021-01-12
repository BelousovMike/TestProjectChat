using MessageSender.Exceptions;
using MessageSender.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System.Net;

namespace UnitTestsProject
{
    [TestFixture]
    public class MessageSenderUnitTests
    {
        [Test]
        public void Test_Connect_GoodConnect()
        {
            var stubConnection = Substitute.For<ISenderConnection>();
            stubConnection.IsConnected.Returns(true);
            var messageSender = new MessageSender.MessageSender(stubConnection);

            messageSender.Connect(IPAddress.Any, 5050);

            Assert.IsTrue(messageSender.IsConnected);
        }

        [Test]
        public void Test_Connect_ReceivedConnect()
        {
            var mockConnection = Substitute.For<ISenderConnection>();
            var messageSender = new MessageSender.MessageSender(mockConnection);

            messageSender.Connect(IPAddress.Any, 5050);

            mockConnection.Received().Connect(Arg.Any<IPAddress>(), Arg.Any<int>());
        }
    }
}
