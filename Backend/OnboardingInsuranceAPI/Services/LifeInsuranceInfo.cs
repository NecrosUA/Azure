using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Services;

public class LifeInsuranceInfo
{
    public DateTime ExpDate { get; set; } //Expiration date
    public bool IsValid { get; set; } //Valid or not
    public string InformationNote { get; set; } //Note about insurancr from agent
    public int? YearlyContribution { get; set; }
}
