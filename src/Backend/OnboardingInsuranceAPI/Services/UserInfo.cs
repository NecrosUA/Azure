using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OnboardingInsuranceAPI.Services;

public class UserInfo
{
    [Key]
    [JsonProperty("pid")]
    public string Pid { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("surname")]
    public string Surname { get; set; }
    [JsonProperty("birthdate")]
    public string Birthdate { get; set; }
    [JsonProperty("birthNumber")]
    public string BirthNumber { get; set; }
    [JsonProperty("mobileNumber")]
    public string MobileNumber { get; set; }
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("address1")]
    public string Address1 { get; set; }
    [JsonProperty("address2")]
    public string Address2 { get; set; }
    [JsonProperty("profileImage")]
    public string ProfileImage { get; set; }
    [JsonProperty("carInsurance")]
    public CarInsuranceInfo CarInsurance { get; set; }
    [JsonProperty("lifeInsurance")]
    public LifeInsuranceInfo LifeInsurance { get; set; }
    [JsonProperty("pensionInsurance")]
    public PensionInsuranceInfo PensionInsurance { get; set; }
}
