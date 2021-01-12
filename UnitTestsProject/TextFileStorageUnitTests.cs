using Logger;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTestsProject
{
    public class TestTextFileStorage : TextFileStorage
    {
        public string WritedString = string.Empty;

        public TestTextFileStorage(string fileName) : base(fileName, false)
        { }

        protected override string ReadFromFile()
        {
            return "one\r\ntwo\r\nthree\r\n";
        }

        protected override void WriteInFile(string stringWrite)
        {
            WritedString = stringWrite;
        }
    }


    [TestFixture]
    public class TextFileStorageUnitTests
    {
        [Test]
        public void Test_Sort_SortAscending()
        {
            var fileWorker = new TextFileStorage("myfile.log", false);
            var list = new List<string>() { "c", "a", "b" };

            var result = fileWorker.Sort(list);

            Assert.AreEqual("a", result[0]);
            Assert.AreEqual("b", result[1]);
            Assert.AreEqual("c", result[2]);
        }

        [Test]
        public void Test_ReadStrings()
        {
            var fileWorker = new TestTextFileStorage("myfile.log");

            var messages = fileWorker.ReadStrings();

            Assert.AreEqual(messages[0], "one");
            Assert.AreEqual(messages[1], "two");
            Assert.AreEqual(messages[2], "three");
        }

        [Test]
        public void Test_WriteString()
        {
            var fileWorker = new TestTextFileStorage("myfile.log");
            var message = "abc";

            fileWorker.WriteString(message);
            var result = fileWorker.WritedString;

            Assert.AreEqual("abc\r\none\r\nthree\r\ntwo", result);
        }
    }
}
