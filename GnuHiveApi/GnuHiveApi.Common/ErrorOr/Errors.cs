using ErrorOr;

namespace GnuHiveApi.Common.ErrorOr;

public static partial class Errors
{
    public static Error Unauthorized(string description) => Error.Custom(401, "General.Unauthorized", description);
    public static Error Forbidden(string description) => Error.Custom(403, "General.Forbidden", description);
    public static Error TooManyRequests(string description = "") => Error.Custom(default, "Http.TooManyRequests", description);
    public static ErrorOr<Success> Cancellation(string description) =>Error.Custom(400, "General.Cancelled", description);
}