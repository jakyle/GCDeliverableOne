using GrandCircusEntry.Workflows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GrandCircusEntry
{
    class AppService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IApplicationLifetime _appLifetime;
        private readonly IWorkflow _workflow;


        public AppService(ILogger<AppService> logger, IApplicationLifetime appLifetime, IWorkflow workflow)
        {
            // Dependency Injection through the DI container.
            _logger = logger;
            _appLifetime = appLifetime;
            _workflow = workflow;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // logg the starting of the app and register the lifetime
            // cycles of the app.
            _logger.LogInformation("App started");

            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;
        }


        private void OnStarted()
        {
            try
            {
                // runs the workflow object, which contains the workflow and console writing of the app
                _workflow.Run();
            }
            catch (Exception e)
            {
                // if theres an error, log it
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        private void OnStopping()
        {
            // one of the lifecycles, if we need to do something on stopping, we can do it here.
        }

        private void OnStopped()
        {
            // when the app shuts down, stop the application
            _appLifetime.StopApplication();
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            // log that app is closing. actually more useful if this was a hosted app and your 
            // app closed for some reason, you need to know when it closed so you can investigate it.
            //  in the case of this program, this kind of logging is more or less just a proof of concept.
            _logger.LogInformation("Application ending...");

            return Task.CompletedTask;
        }
    }
}
