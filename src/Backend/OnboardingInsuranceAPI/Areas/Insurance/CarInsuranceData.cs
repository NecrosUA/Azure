using System;
using System.Text.Json.Serialization;
using OnboardingInsuranceAPI.Services;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public record CarInsuranceData
{
    [JsonPropertyName("expDate")]
    public DateOnly? ExpirationDate { get; init; }
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
    public DateOnly? LastService { get; init; }
    [JsonPropertyName("year")]
    public int? YearOfProduction { get; init; }
    [JsonPropertyName("insuranceId")]
    public string? InsuranceId { get; init;}
}
