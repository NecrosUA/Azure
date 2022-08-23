using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public record ContributionDataResponse
{
    [JsonPropertyName("expDate")]
    public DateOnly ExpirationDate { get; init; }
    [JsonPropertyName("yearlyContribution")]
    public decimal? YearlyContribution { get; init; }
}
