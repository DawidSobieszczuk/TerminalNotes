using Spectre.Console.Cli;

namespace DawidSobieszczuk.TerminalNotes.Console.Commands
{
    internal class SearchNotesCommand : AsyncCommand<SearchNotesCommand.Settings>
    {
        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<keyword>")]
            public string Keyword { get; set; } = null!;
            [CommandOption("--tags|-t")]
            public List<string> Tags { get; set; } = [];
        }

        public override Task<int> ExecuteAsync(CommandContext context, Settings settings)
        {
            throw new NotImplementedException();
        }

    }
}
