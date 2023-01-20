using System.Text.Json.Serialization;

namespace Infrastructure.Models;

public class ExchangeRateModel
{
    [JsonPropertyName("baseCurrency")]
    public string BaseCurrency { set; get; }
    
    [JsonPropertyName("currency")]
    public string Currency { set; get; }
    
    [JsonPropertyName("saleRateNB")]
    public double SaleRateNb { set; get; }
    
    [JsonPropertyName("purchaseRateNB")]
    public double PurchaseRateNb { set; get; }
    
    [JsonPropertyName("saleRate")]
    public double SaleRate { set; get; }
    
    [JsonPropertyName("purchaseRate")]
    public double PurchaseRate { set; get; }
}

public class ExchangeRateRoot
{
    public static string[] AvailableCurrencies = {"USD" , "EUR" , "CHF" , "GBP" , "PLZ" , "SEK" , "XAU" , "CAD"};
    
    [JsonPropertyName("date")]
    public string Date { set; get; }
    
    
    [JsonPropertyName("bank")]
    public string Bank { set; get; }
    
    
    [JsonPropertyName("baseCurrency")]
    public double BaseCurrency { set; get; }
    
    [JsonPropertyName("baseCurrencyLit")]
    public string BaseCurrencyLit { set; get; }
    
    [JsonPropertyName("exchangeRate")]
    public List<ExchangeRateModel> ExchangeRate { set; get; }

}