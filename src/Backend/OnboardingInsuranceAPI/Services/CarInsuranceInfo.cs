namespace OnboardingInsuranceAPI.Services;

public class CarInsuranceInfo
{
    public string ExpirationDate { get; set; } 
    public string InformationNote { get; set; } 
    public decimal? YearlyContribution { get; set; }
    public string CarType { get; set; } 
    public string CarBarnd { get; set; } 
    public bool Crashed { get; set; } 
    public bool FirstOwner { get; set; } 
    public string LastService { get; set; } 
    public string YearOfProduction { get; set; }   
}