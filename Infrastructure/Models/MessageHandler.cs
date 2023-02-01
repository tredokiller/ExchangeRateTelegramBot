using System.Text.RegularExpressions;
using Infrastructure.Services.Interfaces;
using Telegram.Bot.Types;

namespace Infrastructure.Models;

public class MessageHandler
{
    private const string DataPattern =
        @"^([3][0-1]|[0][1-9]|[0][1-9]|[1-2][0-9]).([1][0-2]|[0][1-9]).[0-9][0-9][0-9][0-9]$";

    private readonly IJsonParserService _parserService;

    public MessageHandler(IJsonParserService parserService)
    {
        _parserService = parserService ?? throw new ArgumentNullException(nameof(parserService));
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

        if (message.Text == Messages.StartMessage)
        {
            SendMessage(client, Messages.HelloMessage, message.Chat.Id);
        }
        else
        {
            GetExchangeRate(message, client);
        }
    }


    private void SendMessage(ITelegramClient client, string messageText, long chatId)
    {
        client.SendTextMessage(chatId, messageText);
    }


    private void GetExchangeRate(Message message, ITelegramClient client)
    {
        string text = message.Text!;

        string currency = string.Empty;
        string data = string.Empty;

        if (text.Contains(" "))
        {
            currency = GetCurrency(text);
            data = GetData(text);
        }


        if (currency == string.Empty)
        {
            SendMessage(client, Messages.InvalidCodeMessage, message.Chat.Id);
        }

        else if (data == string.Empty)
        {
            SendMessage(client, Messages.InvalidDataMessage, message.Chat.Id);
        }

        else
        {
            TryToGetExchangeRate(data, currency, client, message);
        }
    }


    private void TryToGetExchangeRate(string data, string currency, ITelegramClient client, Message message)
    {
        try
        {
            ExchangeRateRootModel rate = _parserService.ParseToExchangeRate(data);

            var exchangeRate = rate.ExchangeRate.Where(i => i.Currency == currency).FirstOrDefault();

            SendMessage(client,
                $"{Messages.PurchaseRateMessage + currency} на {data} {Messages.AmountMessage + exchangeRate!.PurchaseRate}\n{Messages.SellRateMessage + Messages.AmountMessage + exchangeRate.SaleRate}",
                message.Chat.Id);
        }

        catch
        {
            SendMessage(client, Messages.ParseExceptionMessage, message.Chat.Id);
        }
    }


    private string GetCurrency(string text)
    {
        string currency = text.Substring(0, text.IndexOf(" ", StringComparison.Ordinal)).ToUpper();


        bool currencyIsValid = ExchangeRateRootModel.AvailableCurrencies.Any(currency.Contains);

        if (!currencyIsValid)
        {
            currency = string.Empty;
        }

        return currency;
    }


    private string GetData(string text)
    {
        string data = String.Empty;

        int firstDataIndex = text.LastIndexOf(" ", StringComparison.Ordinal) + 1;
        int lastDataIndex = firstDataIndex + 10;


        if (lastDataIndex <= text.Length)
        {
            data = text.Substring(firstDataIndex, 10);
            Regex reg = new Regex(DataPattern);

            if (!reg.IsMatch(data))
            {
                data = string.Empty;
            }
        }

        return data;
    }
}