using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Spectre.Console.Cli.Extensions.DependencyInjection;
using Spectre.Console.Cli;
using DawidSobieszczuk.TerminalNotes.Data;
using DawidSobieszczuk.TerminalNotes.Core.Services;
using DawidSobieszczuk.TerminalNotes.Data.Interfaces;
using DawidSobieszczuk.TerminalNotes.Data.DataProviders;
using DawidSobieszczuk.TerminalNotes.Console.Commands;

namespace DawidSobieszczuk.TerminalNotes.Console
{
    class Program
    {       
        public static async Task<int> Main(string[] args)
        {
            var dbFilePath = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                            ".tnotes");

            if (!Directory.Exists(dbFilePath))
            {
                Directory.CreateDirectory(dbFilePath);
            }

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json");
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<AppDbContext>(options => {
                        options.UseSqlite("Data Source =" + Path.Combine(dbFilePath,
                            hostContext.Configuration["DatabaseSettings:DbFileName"] ?? "data.db"
                            ));
                    });
                    services.AddDbContextFactory<AppDbContext>();

                    services.AddScoped<IAppDataProvider, DatabaseAppDataProvider>();
                    services.AddScoped<NotesService>();

                    services.AddSingleton<ICommandApp>(
                        _ =>
                        {
                            var app = new CommandApp<RunCommand>(new DependencyInjectionRegistrar(services));
                            app.Configure(config =>
                            {
                                config.PropagateExceptions();
                                config.ValidateExamples();
                                config.SetApplicationName("tnotes");
                                config.SetApplicationVersion("1.0.0");
                                config.AddCommand<AddNoteCommand>("add")
                                    .WithDescription("Add a new note. Note content should be in quotes.")
                                    .WithExample(["add", "\"This is a sample note.\""])
                                    .WithExample(["add", "\"This is a sample note with tags.\"", "--tags", "tag1,tag2"]);
                                config.AddCommand<ShowNotesCommand>("show")
                                    .WithDescription("Display notes based on the provided options. By default, it shows today's notes.")
                                    .WithExample(["show"])
                                    .WithExample(["show", "--yesterday"])
                                    .WithExample(["show", "--week"]);
                                config.AddCommand<SearchNotesCommand>("search")
                                    .WithDescription("Search for notes containing a specific keyword.")
                                    .WithExample(["search", "keyword"])
                                    .WithExample(["search", "keyword", "--tags", "tag1,tag2"]);
                            });

                            return app;
                        }
                    );

                })
                .Build();

            using var serviceScope = host.Services.CreateScope();

            var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            var app = host.Services.GetRequiredService<ICommandApp>();
            return await app.RunAsync(args);
        }
    }
}