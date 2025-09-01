using Spectre.Console.Cli;

namespace DawidSobieszczuk.TerminalNotes.Console.Commands
{
    internal class ShowNotesCommand : AsyncCommand<ShowNotesCommand.Settings>
    {
        public class Settings : CommandSettings
        {
            [CommandOption("--yesterday")]
            public bool Yesterday { get; set; } = false;
            [CommandOption("--week")]
            public bool Week { get; set; } = false;
        }
        public override Task<int> ExecuteAsync(CommandContext context, Settings settings)
        {
            throw new NotImplementedException();
        }
    }
}
