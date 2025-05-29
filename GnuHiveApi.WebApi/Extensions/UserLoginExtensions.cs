namespace GnuHiveApi.WebApi.Extensions;

public static class UserLoginExtensions
{
    public static string? GetHeaderToken(this IHttpContextAccessor contextAccessor, string tokenName)
    {
        if (contextAccessor.HttpContext != null)
        {
            return contextAccessor.HttpContext.Request.Headers[tokenName];
        }
        return null;
    }
}