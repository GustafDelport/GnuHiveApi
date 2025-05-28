using System.Globalization;

namespace GnuHiveApi.Common.Utils;

public static class StringUtils
{
    private static Random _random = new Random();
    public const string NumericAlphabet = "0123456789";
    public const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
    
    public static string? RemoveWhiteSpaces(string? input)
    {
        return string.IsNullOrWhiteSpace(input) ? input : input.Replace(" ", "");
    }

    public static string? AddPrefixIfNotExists(string? input, string prefix)
    {
        if (input is null)
        {
            return input;
        }

        return input.StartsWith(prefix) ? input : prefix + input;
    }

    public static string? RemovePrefix(string? input, string prefix)
    {
        if (input is null)
        {
            return input;
        }

        return !input.StartsWith(prefix) ? input : input.Substring(prefix.Length, input.Length - prefix.Length);
    }

    public static string? ExtractFirst(string? input, int totalCharacters)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        if (totalCharacters > input.Length)
        {
            return input;
        }

        return input.Substring(0, totalCharacters);
    }

    public static string GenerateRandomString(int length, string alphabet)
    {
        return new string(
            Enumerable.Repeat(0, length)
                .Select(x => alphabet[_random.Next(0, alphabet.Length)])
                .ToArray()
        );
    }

    public static string GenerateRandomNumericString(int length)
    {
        return new string(
            Enumerable.Repeat(0, length)
                .Select(x => NumericAlphabet[_random.Next(0, NumericAlphabet.Length)])
                .ToArray()
        );
    }

    public static string UpperCaseFirstLetter(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        var stringArray = input.ToCharArray();

        if (char.IsLower(stringArray[0]))
        {
            stringArray[0] = char.ToUpper(stringArray[0]);
        }

        return new string(stringArray);
    }

    public static string ConvertToTitleCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }
        
        var result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
        
        return result;
    }

    public static string ConvertToYesNo(bool boolValue,  bool shortVersion = false)
    {
        if (shortVersion)
        {
            return boolValue ? "Y" : "N";
        }
        return boolValue ? "Yes" : "No";
    }

    public static string ShortenLength(string source, int length,  bool trimEnd = false)
    {
        var result = source[..Math.Min(source.Length, length)];
        return trimEnd ? result.TrimEnd() : result;
    }
}