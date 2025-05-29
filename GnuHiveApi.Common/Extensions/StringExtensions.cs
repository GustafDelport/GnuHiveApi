namespace GnuHiveApi.Common.Extensions;

public static class StringExtensions
{
    public static bool IsEqualIgnoreCase(this string? valueA, string? valueB)
    {
        return valueA?.Equals(valueB, StringComparison.InvariantCultureIgnoreCase) ?? valueB is null;
    }
    
    public static bool ContainsIgnoreCase(this string? valueA, string? part)
    {
        if (part == null && valueA == null)
        {
            return true;
        }
        
        if (part == null)
        {
            return false;
        }
        
        return valueA?.Contains(part, StringComparison.InvariantCultureIgnoreCase) ?? false;
    }
}