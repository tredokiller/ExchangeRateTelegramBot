using Infrastructure.Models;
using Infrastructure.Services.Interfaces;
using Telegram.Bot.Types;

namespace Infrastructure.Services;

public class MessageHandlerService : IMessageHandlerService
{
    private readonly MessageHandler _messageHandlerModel;

    public MessageHandlerService(IJsonParserService parserService)
    {
        if (parserService == null)
        {
            throw new ArgumentNullException(nameof(parserService));
        }

        _messageHandlerModel = new MessageHandler(parserService);
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