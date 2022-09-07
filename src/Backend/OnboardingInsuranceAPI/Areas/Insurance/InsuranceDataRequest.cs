using System.Text.Json.Serialization;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public record InsuranceDataRequest
{
    [JsonPropertyName("pid")]
    public string Pid { get; init; }

    [JsonPropertyName("carInsurance")]
    public CarInsuranceData CarInsurance { get; init; }
}
