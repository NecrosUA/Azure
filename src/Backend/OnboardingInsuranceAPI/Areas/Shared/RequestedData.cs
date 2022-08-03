using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using OnboardingInsuranceAPI.Services;
using System.Text.Json.Serialization;

namespace OnboardingInsuranceAPI.Areas.Shared;

public record RequestedData
{
    [JsonPropertyName("pid")]
    public string? Pid { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("surname")]
    public string? Surname { get; set; }
    [JsonPropertyName("birthdate")]
    public string? Birthdate { get; set; }
    [JsonPropertyName("birthNumber")]
    public string? BirthNumber { get; set; }
    [JsonPropertyName("mobileNumber")]
    public string? MobileNumber { get; set; }
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [JsonPropertyName("address1")]
    public string? Address1 { get; set; }
    [JsonPropertyName("address2")]
    public string? Address2 { get; set; }
    [JsonPropertyName("profileImage")]
    public string? ProfileImage { get; set; }
    [JsonPropertyName("carInsurance")]
    public CarInsuranceInfo CarInsurance { get; set; }
    [JsonPropertyName("sub")]
    public string? Sub { get; }
}
