using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Services;

public class UserInfo
{
    [Key]
    [JsonPropertyName("pid")]
    public string Pid { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("surname")]
    public string Surname { get; set; }
    [JsonPropertyName("birthdate")]
    public string Birthdate { get; set; }
    [JsonPropertyName("birthNumber")]
    public string BirthNumber { get; set; }
    [JsonPropertyName("mobileNumber")]
    public string MobileNumber { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("address1")]
    public string Address1 { get; set; }
    [JsonPropertyName("address2")]
    public string Address2 { get; set; }
    [JsonPropertyName("profileImage")]
    public string ProfileImage { get; set; }
    [JsonPropertyName("carInsurance")]
    public CarInsuranceInfo CarInsurance { get; set; }
    [JsonPropertyName("lifeInsurance")]
    public LifeInsuranceInfo LifeInsurance { get; set; }
    [JsonPropertyName("pensionInsurance")]
    public PensionInsuranceInfo PensionInsurance { get; set; }
}
