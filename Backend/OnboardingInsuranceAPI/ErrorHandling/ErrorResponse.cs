namespace OnboardingInsuranceAPI.ErrorHandling;

internal record ErrorResponse
{
    public int Code { get; init; }
    public string Message { get; init; }
}

