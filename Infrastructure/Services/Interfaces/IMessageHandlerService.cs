using Telegram.Bot.Types;

namespace Infrastructure.Services.Interfaces;

public interface IMessageHandlerService
{
    public void HandleMessage(Message message, ITelegramClient client);
}