using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class UserInfo
{
    public UserInfo(string pid, string birthdate, string birthNumber)
    {
        Pid = pid;
        Birthdate = birthdate;
        BirthNumber = birthNumber;
    }

    public string Pid { get; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Birthdate { get; }
    public string BirthNumber { get; }
    public string MobileNumber { get; set; }
    public string Email { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string ProfileImage { get; set; }
}
