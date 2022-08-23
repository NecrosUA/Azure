using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OnboardingInsuranceAPI.Enums;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public record ContributionDataRequest
{
    [JsonPropertyName("carType")]
    public CarTypes CarType { get; init; }
    [JsonPropertyName("crashed")]
    public bool Crashed { get; init; }
    [JsonPropertyName("year")]
    public int? YearOfProduction { get; init; }
    [JsonPropertyName("firstOwner")]
    public bool FirstOwner { get; init; }
}
