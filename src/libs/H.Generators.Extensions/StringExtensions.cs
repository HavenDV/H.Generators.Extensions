using System.Globalization;
// ReSharper disable MemberCanBePrivate.Global

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
#if NET6_0_OR_GREATER
            _ => string.Concat(input[0].ToString().ToUpper(CultureInfo.InvariantCulture), input.AsSpan(1)),
#else
            _ => input[0].ToString().ToUpper(CultureInfo.InvariantCulture) + input.Substring(1),
#endif
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
        input = input ?? throw new ArgumentNullException(nameof(input));
        
#pragma warning disable CA1308
        return input.ToLowerInvariant() switch
#pragma warning restore CA1308
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            
            // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/
            "abstract" => "@abstract",
            "as" => "@as",
            "base" => "@base",
            "bool" => "@bool",
            "break" => "@break",
            "byte" => "@byte",
            "case" => "@case",
            "catch" => "@catch",
            "cChar" => "@char",
            "checked" => "@checked",
            "class" => "@class",
            "const" => "@const",
            "continue" => "@continue",
            "decimal" => "@decimal",
            "default" => "@default",
            "delegate" => "@delegate",
            "do" => "@do",
            "double" => "@double",
            "else" => "@else",
            "enum" => "@enum",
            "event" => "@event",
            "explicit" => "@explicit",
            "extern" => "@extern",
            "false" => "@false",
            "finally" => "@finally",
            "fixed" => "@fixed",
            "float" => "@float",
            "for" => "@for",
            "foreach" => "@foreach",
            "goto" => "@goto",
            "if" => "@if",
            "implicit" => "@implicit",
            "in" => "@in",
            "int" => "@int",
            "interface" => "@interface",
            "internal" => "@internal",
            "is" => "@is",
            "lock" => "@lock",
            "long" => "@long",
            "namespace" => "@namespace",
            "new" => "@new",
            "null" => "@null",
            "object" => "@object",
            "operator" => "@operator",
            "out" => "@out",
            "override" => "@override",
            "params" => "@params",
            "private" => "@private",
            "protected" => "@protected",
            "public" => "@public",
            "readonly" => "@readonly",
            "ref" => "@ref",
            "return" => "@return",
            "sbyte" => "@sbyte",
            "sealed" => "@sealed",
            "short" => "@short",
            "sizeof" => "@sizeof",
            "stackalloc" => "@stackalloc",
            "static" => "@static",
            "string" => "@string",
            "struct" => "@struct",
            "switch" => "@switch",
            "this" => "@this",
            "throw" => "@throw",
            "true" => "@true",
            "try" => "@try",
            "typeof" => "@typeof",
            "uint" => "@uint",
            "ulong" => "@ulong",
            "unchecked" => "@unchecked",
            "unsafe" => "@unsafe",
            "ushort" => "@ushort",
            "using" => "@using",
            "virtual" => "@virtual",
            "void" => "@void",
            "volatile" => "@volatile",
            "while" => "@while",
            
#pragma warning disable CA1308 // Normalize strings to uppercase
#if NET6_0_OR_GREATER
            _ => string.Concat(input[0].ToString().ToLower(CultureInfo.InvariantCulture), input.AsSpan(1)),
#else
            _ => input[0].ToString().ToLower(CultureInfo.InvariantCulture) + input.Substring(1),
#endif
#pragma warning restore CA1308 // Normalize strings to uppercase
        };
    }

    private static readonly char[] Separator = { '\n' };

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
            separator: "\n",
            values: text
                .NormalizeLineEndings()
                .Split(Separator, StringSplitOptions.None)
                .Where(static line => line.Length == 0 || !line.All(char.IsWhiteSpace)));
    }

    /// <summary>
    /// Normalizes line endings to '\n' or your endings.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="newLine">'\n' by default</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string NormalizeLineEndings(
        this string text,
        string? newLine = null)
    {
        text = text ?? throw new ArgumentNullException(nameof(text));

        var newText = text
            .Replace("\r\n", "\n")
            .Replace("\r", "\n");
        if (newLine != null)
        {
            newText = newText.Replace("\n", newLine);
        }

        return newText;
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
