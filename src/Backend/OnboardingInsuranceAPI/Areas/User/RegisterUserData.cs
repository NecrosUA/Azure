using System.Text.Json.Serialization;

namespace OnboardingInsuranceAPI.Areas.User
{
    public record RegisterUserData
    {
        [JsonPropertyName("sub")]
        public string? Sub { get; init; }
        [JsonPropertyName("email")]
        public string? Email { get; init; }
    }
}
