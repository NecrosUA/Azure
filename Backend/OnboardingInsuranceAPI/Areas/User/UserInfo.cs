using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class UserInfo
{
    //public UserInfo(string pid, string birthdate, string birthNumber)
    //{
    //    Pid = pid;
    //    Birthdate = birthdate;
    //    BirthNumber = birthNumber;
    //}
    [Key]
    public string Pid { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Birthdate { get; set; }
    public string BirthNumber { get; set; }
    public string MobileNumber { get; set; }
    public string Email { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string ProfileImage { get; set; }
}
