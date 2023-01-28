using Infrastructure.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Infrastructure.Services;

public class MessageHandlerService : IMessageHandlerService
{

    private readonly MessageHandler _messageHandlerModel;

    public MessageHandlerService(IJsonParserService _parserService)
    {
        if (_parserService == null)
        {
            throw new ArgumentNullException(nameof(_parserService));
        }
        _messageHandlerModel = new MessageHandler(_parserService);
    }


    public void HandleMessage(Message message, ITelegramClient client)
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message));
        }
        if (client == null)
        {
            throw new ArgumentNullException(nameof(client));
        }
        _messageHandlerModel.HandleMessage(message, client);
    }
}