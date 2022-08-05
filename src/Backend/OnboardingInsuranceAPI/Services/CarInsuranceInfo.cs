using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Services;

public class CarInsuranceInfo
{
    [JsonPropertyName("expDate")]
    public string ExpDate { get; set; } //Expiration date
    [JsonPropertyName("informationNote")]
    public string InformationNote { get; set; } //Note about insurancr from agent
    [JsonPropertyName("yearlyContribution")]
    public decimal? YearlyContribution { get; set; }
    [JsonPropertyName("carType")]
    public string CarType { get; set; } //Type of car "for example sportcar"
    [JsonPropertyName("carBrand")]
    public string CarBarnd { get; set; } //Brand of car "for example Škoda"
    [JsonPropertyName("crashed")]
    public bool Crashed { get; set; } //Crashed or not
    [JsonPropertyName("firstOwner")]
    public bool FirstOwner { get; set; } //First owner of the car or not
    [JsonPropertyName("lastService")]
    public string LastService { get; set; } //Date of last service
    [JsonPropertyName("year")]
    public string Year { get; set; } //Year of prosuction   
}