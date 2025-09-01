using DawidSobieszczuk.TerminalNotes.Core.Services;
using Spectre.Console.Cli;

namespace DawidSobieszczuk.TerminalNotes.Console.Commands
{
    internal class AddNoteCommand(NotesService notesService) : AsyncCommand<AddNoteCommand.Settings>
    {
        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<content>")]
            public string Content { get; set; } = null!;
            [CommandOption("--tags|-t")]
            public List<string> Tags { get; set; } = [];
        }

        public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
        {
            await notesService.AddNote(settings.Content, settings.Tags);

            return 0;
        }

    }
}
