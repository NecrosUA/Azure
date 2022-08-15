using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Enums
{
    public enum CarTypes
    {
        Cabriolet = 500,
        SportCar = 1000,
        Van = 200,
        Micro = 50,
        Sedan = 150,
        CamperVan = 500,
        SuperCar = 2000,
        Coupe = 200,
        Other = 100
    }

    internal static class CarTypesContribution
    {
        public static CarTypes FromString(string type) => type.ToLower() switch
        {
            "cabriolet" => CarTypes.Cabriolet,
            "sport car" => CarTypes.SportCar,
            "van" => CarTypes.Van,
            "micro" => CarTypes.Micro,
            "sedan" => CarTypes.Sedan,
            "camper van" => CarTypes.CamperVan,
            "super car" => CarTypes.SuperCar,
            "coupe" => CarTypes.Coupe,
            _ => CarTypes.Other
        };
    }
}
