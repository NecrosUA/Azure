using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnboardingInsuranceAPI.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CarTypes
    {
        Default = 0,
        SuperCar = 2000,
        SportCar = 1000,
        Cabriolet = 500,
        CamperVan = 500,
        Van = 200,
        Coupe = 200,
        Sedan = 150,
        Micro = 50
    }
}
