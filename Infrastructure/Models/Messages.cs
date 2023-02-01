namespace Infrastructure.Models;

public static class Messages
{
    //User Messages
    public const string StartMessage = "/start";


    //Bot Messages
    public const string HelloMessage =
        "Привіт, я бот що надає курс найбільш поширених валют до гривні!💥\nТи можеш вказати валюту та дату і дізнатися відповідний курс!😎\n\nНаприклад: USD 01.10.2022\n\nУвага! Архів зберігає усі дані за останні 4 роки, але ти все ще можеш спробувати ввести ранішню дату!";

    public const string InvalidDataMessage = "Невірний формат дати, спробуйте ще раз.\nНаприклад: USD 01.10.2022";
    public const string InvalidCodeMessage = "Невірно заданий код валюти, спробуйте ще раз.\nНаприклад: USD 01.10.2022";


    public const string ParseExceptionMessage =
        "Не вдалося надати інформацію за цією датою. Архів зберігає повну інформацію за останні 4 роки";

    public const string PurchaseRateMessage = "Курс на купівлю ";
    public const string SellRateMessage = "Курс на продаж ";

    public const string AmountMessage = "становить: ";
}