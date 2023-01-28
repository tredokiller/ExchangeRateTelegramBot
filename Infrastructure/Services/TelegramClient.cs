using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Infrastructure.Services;

public class TelegramClient : ITelegramClient
{

    private ITelegramBotClient _client;
    
    
    public TelegramClient(string BotToken)
    {
        if (BotToken == null)
        {
            throw new ArgumentNullException(nameof(BotToken));
        }
        
        _client = new TelegramBotClient(BotToken);
    }


    public async Task<Message> SendTextMessage(ChatId chatId, string textMessage)
    {
        if (chatId == null)
        {
            throw new ArgumentNullException(nameof(chatId));
        }

        if (string.IsNullOrEmpty(textMessage))
        {
            throw new ArgumentNullException(nameof(textMessage));
        }
        return await _client.SendTextMessageAsync(chatId, textMessage, cancellationToken: default);
    }
    

    public void StartReceiving(Func<ITelegramBotClient, Update, CancellationToken, Task> updateHandler, Func<ITelegramBotClient, Exception, CancellationToken, Task> pollingErrorHandler, ReceiverOptions? receiverOptions,
        CancellationToken cancellationToken)
    {
        if (updateHandler == null)
        {
            throw new ArgumentNullException(nameof(updateHandler));
        }

        if (cancellationToken == null)
        {
            throw new ArgumentNullException(nameof(cancellationToken));
        }

        if (pollingErrorHandler == null)
        {
            throw new ArgumentNullException(nameof(pollingErrorHandler));
        }

        if (receiverOptions == null)
        {
            throw new ArgumentNullException(nameof(receiverOptions));
        }

        _client.StartReceiving(updateHandler, pollingErrorHandler, receiverOptions, cancellationToken);
    }
}