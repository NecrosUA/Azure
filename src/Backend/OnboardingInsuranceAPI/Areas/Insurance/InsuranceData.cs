using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OnboardingInsuranceAPI.Services;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public record InsuranceData
{
    [JsonPropertyName("pid")]
    public string Pid { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("profileImage")]
    public string? ProfileImage { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("carInsurance")]
    public CarInsuranceInfo CarInsurance { get; set; }
}

