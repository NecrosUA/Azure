using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public record ContributionData
{
    [JsonPropertyName("expDate")]
    public string ExpirationDate { get; set; }
    [JsonPropertyName("yearlyContribution")]
    public double? YearlyContribution { get; set; }
    [JsonPropertyName("carType")]
    public string CarType { get; init; }
    [JsonPropertyName("crashed")]
    public bool Crashed { get; init; }
    [JsonPropertyName("year")]
    public string YearOfProduction { get; init; }
    [JsonPropertyName("firstOwner")]
    public bool FirstOwner { get; init; }
}
