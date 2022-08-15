using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnboardingInsuranceAPI.Areas.User;

public record UserData
{
    [JsonPropertyName("pid")]
    public string Pid { get; init; }
    [JsonPropertyName("name")]
    public string? Name { get; init; }
    [JsonPropertyName("surname")]
    public string? Surname { get; init; }
    [JsonPropertyName("birthdate")]
    public string? Birthdate { get; init; }
    [JsonPropertyName("birthNumber")]
    public string? BirthNumber { get; init; }
    [JsonPropertyName("mobileNumber")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    public string? MobileNumber { get; init; }
    [JsonPropertyName("email")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; init; }
    [JsonPropertyName("address1")]
    public string? Address1 { get; init; }
    [JsonPropertyName("address2")]
    public string? Address2 { get; init; }
    [JsonPropertyName("profileImage")]
    public string? ProfileImage { get; init; }
}
