using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace H.Generators.Tests.Extensions;

/// <summary>
/// 
/// </summary>
public static class ImmutableArrayExtensions
{
    /// <summary>
    /// Normalizes location separators to '/'.
    /// </summary>
    /// <param name="diagnostics"></param>
    /// <returns></returns>
    public static ImmutableArray<Diagnostic> NormalizeLocations(
        this ImmutableArray<Diagnostic> diagnostics)
    {
        return diagnostics
            .Select(static diagnostic => diagnostic.Location.ToString().Contains('\\')
                ? Diagnostic.Create(
                    diagnostic.Descriptor,
                    location: Location.Create(
                        filePath: diagnostic.Location.GetLineSpan().Path.Replace('\\', '/'),
                        textSpan: diagnostic.Location.SourceSpan,
                        lineSpan: diagnostic.Location.GetLineSpan().Span),
                    additionalLocations: diagnostic.AdditionalLocations,
                    properties: diagnostic.Properties)
                : diagnostic)
            .ToImmutableArray();
    }
}
