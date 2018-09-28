using GrandCircusEntry.Utility;
using GrandCircusEntry.Workflows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace GrandCircusEntry
{
    // This program is designed to be scalable through developing.  if we need to add more 
    // objects in our DI container, this can be done easily in the program.
    // if we need to run multiple host, this can also be done here.
    // also made a workflow folder, because what if we wanted to run multiple workflows?
    // the only part of this app that was made for scale was all of the utility, that could 
    // totally be better organized. 
    //
    //  Also this app is misisng test, so most companies would out right reject this code 
    // since there is no proof of test.
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostContexts, configApp) =>
                {
                    // basic boilerplate to setup app for the first time.
                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile("appsettings.json", optional: true);

                    // if there are no provided arguments, then why bother adding command line.
                    if (args != null)
                    {
                        // not doing anything with args if we recieve them anyways
                        configApp.AddCommandLine(args);
                    }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // DI (Dependency Inversion) Container, makes all of these 
                    // objects available to each other

                    // adds ILogger, microsofts logging tool.
                    services.AddLogging();

                    // adds options, great for accessing appsetting.json "options"
                    services.AddOptions();

                    // App service that will run the program, this of this as the entry point after configuration.
                    services.AddHostedService<AppService>();

                    // Singletons used throughout the app
                    services.AddSingleton<IWorkflow, NumberMatchingWorkflow>();
                    services.AddSingleton<IMathHelper, MathHelper>();
                    services.AddSingleton<IConsoleHelper, ConsoleHelper>();
                    services.AddSingleton<IValidation, Validation>();

                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    // configure logging to appear in console.
                    configLogging.AddConsole();
                })
                // the final part of configuring the app, builds with all of the 
                // configuration
                .Build();

            // runs the service.
            await host.RunAsync();

        }
    }
}
