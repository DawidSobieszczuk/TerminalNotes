using Spectre.Console.Cli;

namespace DawidSobieszczuk.TerminalNotes.Console.Commands
{
    internal class RunCommand : Command<RunCommand.Settings>
    {
        public class Settings : CommandSettings
        {
            // Define any settings or options for the command here
        }
        public override int Execute(CommandContext context, Settings settings)
        {
            return 0;
        }
    }
}
