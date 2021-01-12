using System;

namespace MessageSender.Commands
{
    public static class Commands
    {
        public static string GetLogCommand()
        {
            return "\x1B GetLog";
        }
    }
}
