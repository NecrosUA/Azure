using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Services;

public class LifeInsuranceInfo
{
    public DateTime ExpirationDate { get; set; } 
    public bool IsValid { get; set; } 
    public string InformationNote { get; set; } 
    public int? YearlyContribution { get; set; }
}
