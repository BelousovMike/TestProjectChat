using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTestsProject")]
namespace Logger
{
    public class TextFileStorage : IStorage
    {
        private string _fileName = string.Empty;
        private object _lockObj;

        public TextFileStorage(string fileName, bool deleteLastFile)
        {
            _fileName = Environment.CurrentDirectory + "\\" + fileName;

            if (deleteLastFile)
                File.Delete(_fileName);   
        }

        public List<string> ReadStrings()
        {
            var textFromFile = ReadFromFile();
            if (string.IsNullOrEmpty(textFromFile))
                return new List<string>();
            else
            {
                return textFromFile.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
        }

        public void WriteString(string message)
        {
            var messages = ReadStrings();
            messages.Add(message);
            messages = Sort(messages);
            var stringWrite = String.Join("\r\n", messages.ToArray());
            WriteInFile(stringWrite);
        }

        protected internal List<string> Sort(List<string> messages)
        {
            return messages.OrderBy(m => m).ToList();
        }

        protected virtual void WriteInFile(string stringWrite)
        {
            try
            {
                lock (_lockObj)
                {
                    File.WriteAllText(_fileName, stringWrite);
                }
            }
            catch
            {

            }
        }

        protected virtual string ReadFromFile()
        {
            if (File.Exists(_fileName))
                return File.ReadAllText(_fileName);
            else
            {
                return string.Empty;
            }
        }
    }
}
