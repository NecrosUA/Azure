using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public record InsuranceDataResponse
{
    [JsonPropertyName("pid")]
    public string Pid { get; init; }
    [JsonPropertyName("name")]
    public string? Name { get; init; }
    [JsonPropertyName("profileImage")]
    public string? ProfileImage { get; init; }
    [JsonPropertyName("email")]
    public string? Email { get; init; }
    [JsonPropertyName("carInsurance")]
    public List<CarInsuranceData>? CarInsurance { get; init; }
}

