using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Infrastructure.Services;

public interface ITelegramClient
{ 
    public Task<Message> SendTextMessage(ChatId chatId, string textMessage);
    
    public void StartReceiving(
        Func<ITelegramBotClient,Update,CancellationToken,Task> updateHandler , 
        Func<ITelegramBotClient,Exception,CancellationToken,Task> pollingErrorHandler , 
        ReceiverOptions? receiverOptions,
        CancellationToken cancellationToken);

}