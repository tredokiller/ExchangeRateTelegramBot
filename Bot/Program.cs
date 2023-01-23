using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bot;


public static class Program
{
    private static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var bot = ActivatorUtilities.CreateInstance<Bot>(host.Services);
    }


    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((context, configuration) =>
        {
            configuration.Sources.Clear();
            configuration.AddJsonFile("appsettings.json" , optional: true, reloadOnChange: true);
        });
    }
}