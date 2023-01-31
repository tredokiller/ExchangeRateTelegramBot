using Telegram.Bot.Types;

namespace Infrastructure.Services;

public interface IMessageHandlerService
{
    public void HandleMessage(Message message, ITelegramClient client);
}