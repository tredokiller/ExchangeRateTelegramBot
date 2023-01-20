using System.Text.RegularExpressions;
using Infrastructure.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Infrastructure.Models;

public class MessageHandlerModel
{
    
    private const string DataPattern = @"^([3][0-1]|[0][1-9]|[0][1-9]|[1-2][0-9]).([1][0-2]|[0][1-9]).[0-9][0-9][0-9][0-9]$";


    private readonly IJsonParserService _parserService = new JsonParserService();


    public void HandleMessage(Message message, ITelegramBotClient client)
    {

        if (message.Text == Messages.StartMessage)
        {
            SendMessage(client, Messages.HelloMessage, message.Chat.Id);
        }
        else
        {
            GetExchangeRate(message, client);
        }

    }


    private async void SendMessage(ITelegramBotClient client , string messageText , long chatId)
    {
        Message sentMessage = await client.SendTextMessageAsync(
            chatId: chatId,
            text: messageText,
            cancellationToken: default);
    }



    private void GetExchangeRate(Message message , ITelegramBotClient client)
    {
        string text = message.Text;

            string currency = string.Empty;
            string data = string.Empty;
            
            
            if (text.Contains(" "))
            {
                currency = text.Substring(0, text.IndexOf(" ")).ToUpper();


                bool currencyIsValid = ExchangeRateRoot.AvailableCurrencies.Any(currency.Contains);

                if (!currencyIsValid)
                {
                    currency = string.Empty;
                }
                

                int firstDataIndex = text.LastIndexOf(" ") + 1;
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
            }


            if (currency == string.Empty)
            {
                SendMessage(client, Messages.InvalidCodeMessage , message.Chat.Id);
            }

            else if (data == string.Empty)
            {
                SendMessage(client, Messages.InvalidDataMessage , message.Chat.Id);
            }
            
            else
            {
                
                try
                {
                    ExchangeRateRoot rate = _parserService.Parse(data);
                    
                    var exchangeRate = rate.ExchangeRate.Where(i=> i.Currency == currency).FirstOrDefault();
                    
                    SendMessage(client, $"{Messages.PurchaseRateMessage + currency} на {data} {Messages.AmountMessage + exchangeRate.PurchaseRate}\n{Messages.SellRateMessage + Messages.AmountMessage + exchangeRate.SaleRate}", message.Chat.Id);
                }

                catch (Exception e)
                {
                    SendMessage(client, Messages.ParseExceptionMessage, message.Chat.Id);
                }
                
            }
    }
    }
    
    
    