using System;
using System.Collections.Generic;

namespace YmlParser.Commands
{
    public class CommandManager
    {
        private readonly Dictionary<string, ICommand> commandInfos = new Dictionary<string, ICommand>();

        public void Register(string commandName, ICommand command)
        {
            if (String.IsNullOrWhiteSpace(commandName))
                throw new ArgumentNullException(nameof(commandName));

            if (command == null)
                throw new ArgumentNullException(nameof(command));

            commandInfos.Add(commandName, command);
        }

        public CommandResult Run(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            if (args.Length == 0)
                return new CommandResult("Не передано ни одного аргумента");

            string commandName = args[0];
            if (String.IsNullOrWhiteSpace(commandName))
                return new CommandResult("Комманда не может быть пустой строкой");

            if (commandInfos.TryGetValue(commandName, out ICommand command))
                return command.Execute(args);

            return new CommandResult($"Комманда {commandName} недоступна");
        }
    }
}
