using Infrastructure.Models;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

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
        AppSettings? appSettings = null;
        var builder = Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((context, configuration) =>
            {
                configuration.Sources.Clear();
                configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                appSettings = configuration.Build().GetRequiredSection("Settings").Get<AppSettings>();
            })
            .ConfigureServices((collection =>
            {
                collection.AddSingleton<ICommunication, ConsoleCommunicationService>();
                collection.AddSingleton<AppSettings>(settigs => appSettings!);
                collection.AddSingleton<HttpClient>();
                collection.AddSingleton<IPrivatBankHttpClient, PrivatBankHttpClient>();
                collection.AddSingleton<IJsonParserService, JsonParserService>();
                collection.AddSingleton<ITelegramClient, TelegramClient>(client =>
                    new TelegramClient(appSettings!.BotToken));
                collection.AddSingleton<IMessageHandlerService, MessageHandlerService>();
            }));
        return builder;
    }
}