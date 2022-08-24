using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace OnboardingInsuranceAPI.Converters;

public class DateOnlyConverter : ValueConverter<DateOnly, string>
{
    public DateOnlyConverter() : base(v => v.ToString("yyyy-MM-dd"), v => DateOnly.ParseExact(v, "yyyy-MM-dd"))
    {
    }
}
