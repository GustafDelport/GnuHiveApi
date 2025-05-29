using System.Text;

namespace GnuHiveApi.Common.Extensions;

public static class ExceptionExtensions
{
    public static string GetFullErrorMessage(this Exception source)
    {
        var sb = new StringBuilder(source.Message);

        var nextException = source.InnerException;

        while (nextException != null)
        {
            sb.AppendLine($"Inner exception: {nextException.Message}");

            nextException = nextException.InnerException;
        }

        return sb.ToString();
    }

    public static string GetFullStackTrace(this Exception source)
    {
        var sb = new StringBuilder(source.StackTrace);

        var nextException = source.InnerException;

        while (nextException != null)
        {
            sb.AppendLine($"Inner exception: {nextException.StackTrace}");

            nextException = nextException.InnerException;
        }

        return sb.ToString();
    }
}