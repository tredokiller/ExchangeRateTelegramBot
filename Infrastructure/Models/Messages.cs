namespace Infrastructure.Models;

public class Messages
{
    
    //User Messages
    public static readonly string StartMessage = "/start";
    
    
    //Bot Messages
    public static readonly string HelloMessage = "Привіт, я бот що надає курс найбільш поширених валют до гривні!💥\nТи можеш вказати валюту та дату і дізнатися відповідний курс!😎\n\nНаприклад: USD 01.10.2022\n\nУвага! Архів зберігає усі дані за останні 4 роки, але ти все ще можеш спробувати ввести ранішню дату!";
    
    public static readonly string InvalidDataMessage = "Невірний формат дати, спробуйте ще раз.\nНаприклад: USD 01.10.2022";
    public static readonly string InvalidCodeMessage = "Невірно заданий код валюти, спробуйте ще раз.\nНаприклад: USD 01.10.2022";


    public static readonly string ParseExceptionMessage =
        "Не вдалося надати інформацію за цією датою. Архів зберігає повну інформацію за останні 4 роки";
    
    public static readonly string PurchaseRateMessage = "Курс на купівлю ";
    public static readonly string SellRateMessage = "Курс на продаж ";
    
    public static readonly string AmountMessage = "становить: ";

}