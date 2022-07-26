using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Services;

public class CarInsuranceInfo
{
    public DateTime ExpDate { get; set; } //Expiration date
    public bool IsValid { get; set; } //Valid or not
    public string InformationNote { get; set; } //Note about insurancr from agent
    public int? YearlyContribution { get; set; }
    public string CarType { get; set; } //Type of car "for example sportcar" 
    public string CarBarnd { get; set; } //Brand of car "for example Škoda"
    public bool Crashed { get; set; } //Crashed or not
    public DateTime LastService { get; set; } //Date of last service
    public int? CarLevel { get; set; } //From 1 to 10 by expensiveness
    public int? Year { get; set; } //Year of prosuction   
}
