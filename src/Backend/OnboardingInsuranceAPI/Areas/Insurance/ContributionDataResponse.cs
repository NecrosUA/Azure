using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.Insurance
{
    public record ContributionDataResponse
    {
        [JsonPropertyName("expDate")]
        public string ExpirationDate { get; init; }
        [JsonPropertyName("yearlyContribution")]
        public double? YearlyContribution { get; init; }
    }
}
