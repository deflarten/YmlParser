using System;

namespace YmlParser.Commands
{
    public class CommandResult
    {
        public string Message { get; }
        public CommandResult(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }
}
