using System.Net;
using System.Text.Json;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace GnuHiveApi.WebApi.Extensions;

public static class ControllerExtensions
{
    public static ObjectResult FromError(this ControllerBase controller, Error error)
    {
        var details = controller.ProblemDetailsFactory.CreateProblemDetails(
            controller.HttpContext,
            (int)MapToHttpCode((int)error.Type),
            detail: error.Description);

        return new ObjectResult(details);
    }

    public static ObjectResult FromErrors(this ControllerBase controller, IEnumerable<Error> errors)
    {
        var enumerable
            = errors as Error[] ?? errors.ToArray();

        var details = controller.ProblemDetailsFactory.CreateProblemDetails(
            controller.HttpContext,
            (int)MapToHttpCode((int)enumerable.First().Type),
            detail: JsonSerializer.Serialize(enumerable.GroupBy(g => g.Code).Select(grouping => new
            {
                Code = grouping.Key,
                Description = string.Join(";", grouping.Select(e => e.Description))
            }).ToDictionary(e => e.Code, e => e.Description), new JsonSerializerOptions()));

        return new ObjectResult(details);
    }
    
    public static byte[] ToByteArray(this IFormFile formFile)
    {
        using (var memoryStream = new MemoryStream())
        {
            formFile.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }

    public static HttpStatusCode MapToHttpCode(int type) =>
        type switch
        {
            (int)ErrorType.Failure => HttpStatusCode.InternalServerError,
            (int)ErrorType.Unexpected => HttpStatusCode.InternalServerError,
            (int)ErrorType.Validation => HttpStatusCode.BadRequest,
            (int)ErrorType.Conflict => HttpStatusCode.Conflict,
            (int)ErrorType.NotFound => HttpStatusCode.NotFound,
            403 => HttpStatusCode.Forbidden,
            _ => HttpStatusCode.InternalServerError
        };
}