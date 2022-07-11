using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public class PensionInsuranceInfo
{
    public DateTime ExpDate { get; set; } //Expiration date
    public bool IsValid { get; set; } //Valid or not
    public string InformationNote { get; set; } //Note about insurancr from agent
    public int? MonthlyContribution { get; set; }
}
