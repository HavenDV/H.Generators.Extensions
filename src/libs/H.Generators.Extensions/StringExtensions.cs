namespace H.Generators.Extensions;

/// <summary>
/// 
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Makes the first letter of the name uppercase.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static string ToPropertyName(this string input)
    {
        return input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input[0].ToString().ToUpper() + input.Substring(1),
        };
    }

    /// <summary>
    /// Makes the first letter of the name lowercase.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static string ToParameterName(this string input)
    {
        return input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input[0].ToString().ToLower() + input.Substring(1),
        };
    }

    /// <summary>
    /// Removes blank lines where there are only spaces.
    /// Used to preserve formatting in code where lines of code may be missing based on conditions.
    /// Just return a string with spaces to remove it.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string RemoveBlankLinesWhereOnlyWhitespaces(this string text)
    {
        text = text ?? throw new ArgumentNullException(nameof(text));

        return string.Join(
            separator: Environment.NewLine,
            values: text
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .Where(static line => line.Length == 0 || !line.All(char.IsWhiteSpace)));
    }

    /// <summary>
    /// Returns the namespace for the selected type's fully qualified name.
    /// </summary>
    /// <param name="fullTypeName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string ExtractNamespace(this string fullTypeName)
    {
        fullTypeName = fullTypeName ?? throw new ArgumentNullException(nameof(fullTypeName));

        return fullTypeName.Substring(0, fullTypeName.LastIndexOf('.'));
    }

    /// <summary>
    /// Returns the simple name(without namespace) for the selected type's fully qualified name.
    /// </summary>
    /// <param name="fullTypeName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string ExtractSimpleName(this string fullTypeName)
    {
        fullTypeName = fullTypeName ?? throw new ArgumentNullException(nameof(fullTypeName));

        return fullTypeName.Substring(fullTypeName.LastIndexOf('.') + 1);
    }

    /// <summary>
    /// Returns selected type's fully qualified name with 'global::' prefix.
    /// </summary>
    /// <param name="fullTypeName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string WithGlobalPrefix(this string fullTypeName)
    {
        fullTypeName = fullTypeName ?? throw new ArgumentNullException(nameof(fullTypeName));

        return $"global::{fullTypeName}";
    }
}
