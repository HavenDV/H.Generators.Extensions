using Microsoft.CodeAnalysis;

namespace H.Generators.Extensions;

/// <summary>
/// 
/// </summary>
public static class AttributeDataExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="attributeData"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static ITypeSymbol? GetGenericTypeArgument(this AttributeData attributeData, int position)
    {
        attributeData = attributeData ?? throw new ArgumentNullException(nameof(attributeData));

        return attributeData.AttributeClass?.TypeArguments.ElementAtOrDefault(position);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attributeData"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TypedConstant GetNamedArgument(this AttributeData attributeData, string name)
    {
        attributeData = attributeData ?? throw new ArgumentNullException(nameof(attributeData));

        return attributeData.NamedArguments
            .FirstOrDefault(pair => pair.Key == name)
            .Value;
    }
}
