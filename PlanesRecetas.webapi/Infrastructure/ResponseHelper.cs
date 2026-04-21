using Joseco.DDD.Core.Results;
using PlanesRecetas.application.Pacientes;
using VaultSharp.V1.SecretsEngines.Identity;

namespace PlanesRecetas.webapi.Infrastructure;

public class ResponseHelper
{

    public static string GetTitle(Error error) =>
       error.Type switch
       {
           ErrorType.Validation => error.Code,
           ErrorType.Problem => error.Code,
           ErrorType.NotFound => error.Code,
           ErrorType.Conflict => error.Code,
           _ => "Server failure"
       };

    public static string GetDetail(Error error) =>
        error.Type switch
        {
            ErrorType.Validation => error.Description,
            ErrorType.Problem => error.Description,
            ErrorType.NotFound => error.Description,
            ErrorType.Conflict => error.Description,
            _ => "An unexpected error occurred"
        };

    public static string GetType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ErrorType.Problem => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
            _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

    public static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

    public static Dictionary<string, object?>? GetErrors(Result result)
    {
        if (result.Error is not ValidationError validationError)
        {
            return null;
        }

        return new Dictionary<string, object?>
            {
                { "errors", validationError.Errors }
            };
    }


    internal static ApiResponse CreateResponse<T>(Result<T> result)
    {
        return new ApiResponse
        {
            IsSuccess = result.IsSuccess,
            IsFailure = !result.IsSuccess,
            // Only assign Value if it's a success to avoid unexpected data
            Value = result.IsSuccess ? result.Value : null,

            // Use the null-conditional operator (?.) to prevent crashes
            Error = result.IsSuccess ? null : new ErrorDetails
            {
                Code = result.Error?.Code ?? "",
                Description = result.Error?.Description ?? "An unknown error occurred.",
                StructuredMessage = result.Error?.StructuredMessage,
                Type = (int)(result.Error?.Type ?? 0)
            }
        };
    }
    internal static ApiResponse CreateErrorResponse(Exception error)
    {
        ApiResponse response=new()
        {
            IsFailure = true,
            IsSuccess = false,
            Error = new ErrorDetails
            {
                Code = "",
                Description = error.Message,
                StructuredMessage = error.Message,
                Type = 0
            }
        };
        return response;
    }

    
}