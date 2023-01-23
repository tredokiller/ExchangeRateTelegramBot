using System.Text.Json.Serialization;

namespace Infrastructure.Models;

public class ExchangeRateRootModel
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