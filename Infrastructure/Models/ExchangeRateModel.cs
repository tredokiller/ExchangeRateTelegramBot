using System.Text.Json.Serialization;

namespace Infrastructure.Models;

public class ExchangeRateModel
{
    [JsonPropertyName("baseCurrency")] public string BaseCurrency { set; get; }

    [JsonPropertyName("currency")] public string Currency { set; get; }

    [JsonPropertyName("saleRateNB")] public double SaleRateNb { set; get; }

    [JsonPropertyName("purchaseRateNB")] public double PurchaseRateNb { set; get; }

    [JsonPropertyName("saleRate")] public double SaleRate { set; get; }

    [JsonPropertyName("purchaseRate")] public double PurchaseRate { set; get; }
}