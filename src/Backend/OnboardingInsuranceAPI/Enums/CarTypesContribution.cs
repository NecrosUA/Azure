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
        public static int FromString(string type) => type.ToLower() switch
        {
            "cabriolet" => (int)CarTypes.Cabriolet,
            "sport_car" => (int)CarTypes.SportCar,
            "van" => (int)CarTypes.Van,
            "micro" => (int)CarTypes.Micro,
            "sedan" => (int)CarTypes.Sedan,
            "camper_van" => (int)CarTypes.CamperVan,
            "super_car" => (int)CarTypes.SuperCar,
            "coupe" => (int)CarTypes.Coupe,
            _ => (int)CarTypes.Default
        };
    }
}
