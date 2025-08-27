using System.CommandLine;

namespace DawidSobieszczuk.TerminalNotes.Console.Services
{
    internal class ConsoleService
    {
        public int Run(string[] args)
        {
            var rootCommand = new RootCommand("a simple tool for managing short notes in the terminal.");

            var addCommand = new Command("add", "Note content should be in quotes.");
            var addContentArgument = new Argument<string>("message")
            {
                Description = "The content of the note to be added."
            };
            var addTagsOption = new Option<string[]>("--tags", "-t");
            addCommand.Add(addTagsOption);
            addCommand.Add(addContentArgument);

            addCommand.SetAction(parseResult =>
            {
                string content = parseResult.GetValue<string>("message") ?? throw new Exception();
                // Dodanie notki
            });

            var showCommand = new Command("show", "Displays notes based on the provided options. By default, it shows today's notes.");
            var showYesterdayOption = new Option<bool>("yesterday");
            var showWeekOption = new Option<bool>("week");
            showCommand.Add(showYesterdayOption);
            showCommand.Add(showWeekOption);

            showCommand.SetAction(parseResult =>
            {
                bool showYesterday = parseResult.GetValue<bool>("yesterday");
                bool showWeek = parseResult.GetValue<bool>("week");
                // Wyświetlenie notek na podstawie opcji
            });

            var searchCommand = new Command("search", "Searches for notes containing a specific keyword.");
            var searchKeywordArgument = new Argument<string>("keyword")
            {
                Description = "The keyword to search for in notes."
            };
            var searchTagsOption = new Option<string[]>("--tags", "-t");
            searchCommand.Add(searchTagsOption);
            searchCommand.Add(searchKeywordArgument);
            searchCommand.SetAction(parseResult =>
            {
                string keyword = parseResult.GetValue<string>("keyword") ?? throw new Exception();
                string[] tags = parseResult.GetValue<string[]>("--tags") ?? Array.Empty<string>();
                // Wyszukanie notek na podstawie słowa kluczowego i opcjonalnych tagów
            });


            rootCommand.Add(addCommand);
            rootCommand.Add(showCommand);
            rootCommand.Add(searchCommand);

            return rootCommand.Parse(args).Invoke();
        }
    }
}
