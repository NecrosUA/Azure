using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnboardingInsuranceAPI.Areas.User
{
    public class ReqData
    {
        public string ReqPid { get; set; }
        public string ReqName { get; set; }
        public string ReqSurname { get; set; }
        public string ReqMobileNumber { get; set; }
        public string ReqEmail { get; set; }
        public string ReqAddress1 { get; set; }
        public string ReqAddress2 { get; set; }
        public string ReqProfileImage { get; set; }
    }
}
