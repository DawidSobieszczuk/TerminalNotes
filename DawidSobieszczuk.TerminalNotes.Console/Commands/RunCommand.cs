using Spectre.Console;
using Spectre.Console.Cli;
using System.Text;

namespace DawidSobieszczuk.TerminalNotes.Console.Commands
{
    internal class RunCommand : AsyncCommand<RunCommand.Settings>
    {
        public class Settings : CommandSettings
        {

        }

        public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new FigletText("Terminal Notes").Centered().Color(Color.Green));

            return await MainMenu(context, settings);
        }

        private async Task<int> MainMenu(CommandContext context, Settings settings)
        {
            var option = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
                   .AddChoices([
                       "Create Note",
                        "View Notes",
                        "Search Notes",
                        "Exit"
                   ]));

            switch (option)
            {
                case "Create Note":
                    return await CreateNoteMenu(context, settings);
                case "View Notes":
                    return await ViewMenu(context, settings);
                case "Search Notes":
                    return 0;
                case "Exit":
                    AnsiConsole.MarkupLine("[green]Exiting Terminal Notes. Goodbye![/]");
                    return 0;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid option selected.[/]");
                    return 1;
            }
        }

        private async Task<int> CreateNoteMenu(CommandContext context, Settings settings)
        {
            var maxRows = System.Console.WindowHeight;
            var maxCols = System.Console.WindowWidth;

            var title = new Rule("Create Note")
                .RuleStyle(new Style(Color.Black, Color.White))
                .NoBorder()
                .LeftJustified();

            var footer = new Columns(new Markup("[gray]CTRL + S[/] Save"),  new Markup("[gray]CTRL + X[/] Exit"));

            var buffer = new StringBuilder();
            while (true)
            {

                AnsiConsole.Clear();
                AnsiConsole.Write(title);

                AnsiConsole.WriteLine(buffer.ToString());

                for (var i = 1; i < maxRows - 1 - buffer.Length; i++)
                    AnsiConsole.WriteLine(" ");

                AnsiConsole.Write(footer);


                System.Console.SetCursorPosition(0, 1);
                buffer.AppendLine(System.Console.ReadKey(true).KeyChar.ToString());
            }
        }

        private async Task<int> ViewMenu(CommandContext context, Settings settings)
        {
            var viewOption = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .AddChoices([
                                "All Notes",
                                "By Date",
                                "By Tag",
                                "Back"
                            ]));

            switch (viewOption)
            {
                case "All Notes":
                    return 0;
                case "By Date":
                    return 0;
                case "By Tag":
                    return 0;
                case "Back":
                    return await ExecuteAsync(context, settings);
                default:
                    AnsiConsole.MarkupLine("[red]Invalid option selected.[/]");
                    return 1;
            }
        }
    }
}
