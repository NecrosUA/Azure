using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.ErrorHandling;

public class ExceptionHandlerMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly IHostEnvironment _host;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, IHostEnvironment host)
    {
        _logger = logger;
        _host = host;
    }

    /// <summary>
    /// Gracefully catches, logs, and handles all exceptions from the Azure Functions.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (AggregateException agg) when (agg.InnerException is ApiException ex)
        {
            _logger.LogError(ex, "API Exception: {ex}", ex.Message);

            object? response = null;
            response = new ErrorResponse { Code = (int)ex.ErrorCode, Message = ex.Message };

            await context.SetResponse(ex.ErrorCode.MapToHttpStatusCode(), response);
        }
        catch (AggregateException agg) when (agg.InnerException is ValidationException ex)
        {
            _logger.LogError(ex, "Validation Exception: {ex}", ex.Message);

            object? response = null;
            response = new ErrorResponse { Code = (int)ErrorCode.ValidationFailed, Message = ex.Message };

            await context.SetResponse(HttpStatusCode.BadRequest, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {ex}", ex.Message);

            object? response = null;
            response = new ErrorResponse { Code = (int)ErrorCode.UnhandledException, Message = ex.Message };

            await context.SetResponse(HttpStatusCode.InternalServerError, response);
        }
    }
}
