using System.Text.Json.Serialization;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public record CarInsuranceData
{
    [JsonPropertyName("expDate")]
    public string ExpDate { get; init; } //Expiration date
    [JsonPropertyName("informationNote")]
    public string InformationNote { get; init; } //Note about insurancr from agent
    [JsonPropertyName("yearlyContribution")]
    public decimal? YearlyContribution { get; init; }
    [JsonPropertyName("carType")]
    public string CarType { get; init; } //Type of car "for example sportcar"
    [JsonPropertyName("carBrand")]
    public string CarBarnd { get; init; } //Brand of car "for example Škoda"
    [JsonPropertyName("crashed")]
    public bool Crashed { get; init; } //Crashed or not
    [JsonPropertyName("firstOwner")]
    public bool FirstOwner { get; init; } //First owner of the car or not
    [JsonPropertyName("lastService")]
    public string LastService { get; init; } //Date of last service
    [JsonPropertyName("year")]
    public string Year { get; init; } //Year of prosuction   
}
