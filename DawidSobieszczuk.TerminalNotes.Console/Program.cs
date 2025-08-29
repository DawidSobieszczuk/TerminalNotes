using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DawidSobieszczuk.TerminalNotes.Console.Services;
using DawidSobieszczuk.TerminalNotes.Data;
using Microsoft.EntityFrameworkCore;
using DawidSobieszczuk.TerminalNotes.Core.Services;
using DawidSobieszczuk.TerminalNotes.Data.Interfaces;
using DawidSobieszczuk.TerminalNotes.Data.DataProviders;

namespace DawidSobieszczuk.TerminalNotes.Console
{
    class Program
    {       
        public static int Main(string[] args)
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

                    services.AddScoped<ConsoleService>();
                    services.AddScoped<IAppDataProvider, DatabaseAppDataProvider>();
                    services.AddScoped<NotesService>();
                    
                })
                .Build();

            using var serviceScope = host.Services.CreateScope();

            var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            var consoleService = serviceScope.ServiceProvider.GetRequiredService<ConsoleService>();
            return consoleService.Run(args);
        }
    }
}