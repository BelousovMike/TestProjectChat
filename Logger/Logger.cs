using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class Logger : ILogger
    {
        private IStorage _fileWorker = null;

        public Logger(IStorage fileWorker)
        {
            _fileWorker = fileWorker;
        }

        public void Log(string message)
        {
            _fileWorker.WriteString(message);
        }
        public string ReadLog()
        {
            var strings = _fileWorker.ReadStrings();
            var builder = new StringBuilder();
            foreach (var str in strings)
                builder.AppendLine(str);

            return builder.ToString();
        }
    }
}
