namespace OnboardingInsuranceAPI.Services;

public class CarInsuranceInfo
{
    public string ExpDate { get; set; } //Expiration date
    public string InformationNote { get; set; } //Note about insurancr from agent
    public decimal? YearlyContribution { get; set; }
    public string CarType { get; set; } //Type of car "for example sportcar"
    public string CarBarnd { get; set; } //Brand of car "for example Škoda"
    public bool Crashed { get; set; } //Crashed or not
    public bool FirstOwner { get; set; } //First owner of the car or not
    public string LastService { get; set; } //Date of last service
    public string Year { get; set; } //Year of prosuction   
}