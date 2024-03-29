using System;
using System.ComponentModel.DataAnnotations;

namespace OnboardingInsuranceAPI.Services;

public class UserInfo
{
    [Key]
    public string Pid { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly? Birthdate { get; set; }
    public string BirthNumber { get; set; }
    public string MobileNumber { get; set; }
    public string Email { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string ProfileImage { get; set; }
}
