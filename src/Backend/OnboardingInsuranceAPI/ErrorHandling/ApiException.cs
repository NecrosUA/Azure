using System;

namespace OnboardingInsuranceAPI.ErrorHandling;

public class ApiException : Exception
{
    public ErrorCode ErrorCode { get; }

    public ApiException(ErrorCode errorCode) : base(errorCode.ToString())
        => ErrorCode = errorCode;

    public ApiException(ErrorCode errorCode, string message) : base($"{errorCode}: {message}")
        => ErrorCode = errorCode;
}