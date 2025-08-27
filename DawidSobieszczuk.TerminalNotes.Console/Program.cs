using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DawidSobieszczuk.TerminalNotes.Console.Services;

namespace DawidSobieszczuk.TerminalNotes.Console
{
    class Program
    {       
        public static int Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json");
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<ConsoleService>();
                })
                .Build();

            var consoleService = host.Services.GetRequiredService<ConsoleService>();
            return consoleService.Run(args);
        }
    }
}