using System;
using System.IO;
using System.Threading.Tasks;
using HostedService.service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HostedService
{
    class Program
    {

        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                    .ConfigureHostConfiguration(configHost =>
                    {
                        configHost.SetBasePath(Directory.GetCurrentDirectory());
                        configHost.AddEnvironmentVariables(EnvironmentName.Development);
                        configHost.AddJsonFile("appsettings.json", optional: true);
                        configHost.AddCommandLine(args);
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddHostedService<Test>();
                    })
                    .ConfigureLogging((hostContext, configLogging) =>
                    {
                        configLogging.AddConsole();
                    })
                    .UseConsoleLifetime()
                    .Build();

            await host.RunAsync();
        }
    }


}
