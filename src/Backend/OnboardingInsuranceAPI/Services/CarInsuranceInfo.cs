using System;
using System.ComponentModel.DataAnnotations;

namespace OnboardingInsuranceAPI.Services;

public class CarInsuranceInfo
{
    [Key]
    public string Pid { get; set; }
    public string InsuranceId { get; set; } 
    public DateOnly? ExpirationDate { get; set; } 
    public string InformationNote { get; set; } 
    public decimal? YearlyContribution { get; set; }
    public string CarType { get; set; } 
    public string CarBarnd { get; set; } 
    public bool Crashed { get; set; } 
    public bool FirstOwner { get; set; } 
    public DateOnly? LastService { get; set; } 
    public int? YearOfProduction { get; set; }   
}