using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot;

public class Bot
{
    private readonly IMessageHandlerService _messageHandlerService;
    private readonly ICommunication _communicationService;

    private readonly ITelegramClient _client;

    private readonly CancellationTokenSource _cts = new();

    private readonly ReceiverOptions _receiverOptions = new()
    {
        AllowedUpdates = Array.Empty<UpdateType>()
    };


    public Bot(IConfiguration configuration, ICommunication communicationService, ITelegramClient telegramClient,
        IMessageHandlerService messageHandlerService)
    {
        _client = telegramClient ?? throw new ArgumentNullException(nameof(telegramClient));

        _communicationService = communicationService ?? throw new ArgumentNullException(nameof(communicationService));

        _messageHandlerService =
            messageHandlerService ?? throw new ArgumentNullException(nameof(messageHandlerService));


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


        _messageHandlerService.HandleMessage(message, _client);
        return Task.CompletedTask;
    }


    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
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