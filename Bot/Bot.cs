using Infrastructure.Services;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot;

public class Bot
{
    const string BotToken = "5940614812:AAGeYkg9AssR4ivegSl4dV6hBQqP4kSWRPA";


    private readonly IMessageHandlerService _messageHandlerService;
    
    
    private readonly TelegramBotClient _client;



    private readonly CancellationTokenSource _cts = new();

    private readonly ReceiverOptions _receiverOptions = new()
    {
        AllowedUpdates = Array.Empty<UpdateType>()
    };
    
    

    public Bot()
    {
        _client = new TelegramBotClient($"{BotToken}");

        _messageHandlerService = new MessageHandlerService();

        _client.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: _receiverOptions,
            cancellationToken: _cts.Token
        );
        
        
        Console.ReadLine();
        
        _cts.Cancel();
    }
    

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message)
            return;

        if (message.Text is not { } messageText)
            return;

        
        _messageHandlerService.HandleMessage(message, botClient);

    }


    Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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