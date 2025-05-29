using ErrorOr;

namespace GnuHiveApi.Common.ErrorOr;

public static partial class Errors
{
    public static Error Unauthorized(string description) => Error.Custom(401, "General.Unauthorized", description);
    public static Error Forbidden(string description) => Error.Custom(403, "General.Forbidden", description);
    public static Error TooManyRequests(string description = "") => Error.Custom(default, "Http.TooManyRequests", description);
    public static ErrorOr<Success> Cancellation(string description) =>Error.Custom(400, "General.Cancelled", description);
    
    public static Error FromHttpResponse(HttpResponseMessage response) => Error.Custom((int)response.StatusCode,
        Error.Unexpected().Code, response.ReasonPhrase!);

    public static Error FromHttpResponse(HttpResponseMessage response, string reason) => Error.Custom((int)response.StatusCode,
        Error.Unexpected().Code, reason);
    public static ErrorOr<TOut> Combine<TIn, TOut>(this IEnumerable<ErrorOr<TIn>> errors)
    {
        var finalError = new ErrorOr<TOut>();

        foreach (var error in errors)
        {
            finalError.Errors.AddRange(error.Errors);
        }

        return finalError;
    }
}