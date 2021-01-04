namespace YmlParser.Commands
{
    public interface ICommand
    {
        public CommandResult Execute(string[] args);
    }
}
