namespace H.Generators;

/// <summary>
/// 
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Concatenates strings and cleans up line breaks at the beginning and end of the resulting string.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static string Inject(this IEnumerable<string> values)
    {
        return string.Concat(values)
            .TrimStart('\r', '\n')
            .TrimEnd('\r', '\n');
    }
}
