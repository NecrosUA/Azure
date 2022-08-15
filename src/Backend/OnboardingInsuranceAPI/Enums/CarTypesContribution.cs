using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Enums
{
    public enum CarTypes
    {
        Default = 100,
        SuperCar = 2000,
        SportCar = 1000,
        Cabriolet = 500,
        CamperVan = 500,
        Van = 200,
        Coupe = 200,
        Sedan = 150,
        Micro = 50
    }

    internal static class CarTypesContribution
    {
        public static CarTypes FromString(string type) => type.ToLower() switch
        {
            "cabriolet" => CarTypes.Cabriolet,
            "sport_car" => CarTypes.SportCar,
            "van" => CarTypes.Van,
            "micro" => CarTypes.Micro,
            "sedan" => CarTypes.Sedan,
            "camper_van" => CarTypes.CamperVan,
            "super_car" => CarTypes.SuperCar,
            "coupe" => CarTypes.Coupe,
            _ => CarTypes.Default
        };
    }
}
