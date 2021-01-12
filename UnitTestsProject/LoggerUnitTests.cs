using Logger;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTestsProject
{
    [TestFixture]
    public class LoggerUnitTests
    {
        [Test]
        public void Test_Log_ReceivedWriteString()
        {
            var mockFileWorker = Substitute.For<IStorage>();
            var logger = new Logger.Logger(mockFileWorker);
            var message = "Hello World";

            logger.Log(message);

            mockFileWorker.Received().WriteString(message);
        }

        [Test]
        public void Test_ReadLog_GetString()
        {
            var stubFileWorker = Substitute.For<IStorage>();
            var logger = new Logger.Logger(stubFileWorker);
            stubFileWorker.ReadStrings().Returns(new List<string>() { "one", "two", "three" });

            var result = logger.ReadLog();

             Assert.AreEqual("one\r\ntwo\r\nthree\r\n", result);
        }  
    }
}
