using System.Text.Json.Serialization;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public record CarInsuranceData
{
    [JsonPropertyName("expDate")]
    public string ExpirationDate { get; init; } 
    [JsonPropertyName("informationNote")]
    public string InformationNote { get; init; } 
    [JsonPropertyName("yearlyContribution")]
    public decimal? YearlyContribution { get; init; }
    [JsonPropertyName("carType")]
    public string CarType { get; init; }
    [JsonPropertyName("carBrand")]
    public string CarBarnd { get; init; } 
    [JsonPropertyName("crashed")]
    public bool Crashed { get; init; } 
    [JsonPropertyName("firstOwner")]
    public bool FirstOwner { get; init; } 
    [JsonPropertyName("lastService")]
    public string LastService { get; init; }
    [JsonPropertyName("year")]
    public string YearOfProduction { get; init; } 
}
