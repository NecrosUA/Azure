using System.Net;

namespace OnboardingInsuranceAPI.ErrorHandling;

public enum ErrorCode
{
    InvalidQueryParameters = 399,
    ValidationFailed = 400,
    Unauthorized = 401,
    NotFound = 404,

    DBCallFailed = 501,
    UnhandledException = 502,
}

internal static class ErrorCodeExtensions
{
    internal static HttpStatusCode MapToHttpStatusCode(this ErrorCode errorCode) => errorCode switch
    {
        ErrorCode.ValidationFailed => HttpStatusCode.BadRequest,
        ErrorCode.NotFound => HttpStatusCode.NotFound,
        ErrorCode.Unauthorized => HttpStatusCode.Forbidden,
        ErrorCode.DBCallFailed => HttpStatusCode.InternalServerError,
        ErrorCode.InvalidQueryParameters or ErrorCode.ValidationFailed => HttpStatusCode.BadRequest,
        _ => HttpStatusCode.InternalServerError
    };
}
