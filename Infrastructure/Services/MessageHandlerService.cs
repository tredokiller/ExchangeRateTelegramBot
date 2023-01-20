using Infrastructure.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Infrastructure.Services;

public class MessageHandlerService : IMessageHandlerService
{

    private MessageHandlerModel _messageHandlerModel;
    private IJsonParserService _parserService;
    
    public MessageHandlerService(MessageHandlerModel mHandlerModel = null)
    {
        _messageHandlerModel = mHandlerModel ?? new MessageHandlerModel();
    }
    
    
    public void HandleMessage(Message message, ITelegramBotClient client)
    {
        _messageHandlerModel.HandleMessage(message, client);
    }
}