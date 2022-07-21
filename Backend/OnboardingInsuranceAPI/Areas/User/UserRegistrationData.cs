﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public record UserRegistrationData
(
    [JsonProperty("pid")]
    string Pid,
    [JsonProperty("name")]
    string Name,
    [JsonProperty("surname")]
    string Surname,
    [JsonProperty("birthdate")]
    string Birthdate,
    [JsonProperty("birthNumber")]
    string BirthNumber,
    [JsonProperty("mobileNumber")]
    string MobileNumber,
    [JsonProperty("email")]
    string Email,
    [JsonProperty("address1")]
    string Address1,
    [JsonProperty("address2")]
    string Address2,
    [JsonProperty("profileImage")]
    string ProfileImage
);