using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot;

public class Bot
{

    private readonly IConfiguration _configuration;

    private readonly AppSettings _botSettings;
    
    private readonly IMessageHandlerService _messageHandlerService;
    private readonly IJsonParserService _jsonParserService;
    private readonly ICommunication _communicationService;
    
    private readonly TelegramBotClient _client;
    
    private readonly CancellationTokenSource _cts = new();

    private readonly ReceiverOptions _receiverOptions = new()
    {
        AllowedUpdates = Array.Empty<UpdateType>()
    };
    

    public Bot(IConfiguration configuration, ICommunication communicationService = null)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _communicationService = communicationService ?? new ConsoleCommunicationService();
        
        _botSettings = _configuration.GetRequiredSection("Settings").Get<AppSettings>();
        
        
        
        
        _jsonParserService = new JsonParserService(_botSettings);
        _messageHandlerService = new MessageHandlerService(_jsonParserService);
        
        _configuration = configuration;
        
        _client = new TelegramBotClient(_botSettings.BotToken);
        
        

        _client.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: _receiverOptions,
            cancellationToken: _cts.Token
        );
        
        
        _communicationService.ReadLine();
        
        _cts.Cancel();
    }
    

    private Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message)
            return Task.CompletedTask;

        if (message.Text is not { } messageText)
            return Task.CompletedTask;

        
        _messageHandlerService.HandleMessage(message, botClient);
        return Task.CompletedTask;
    }


    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }

}