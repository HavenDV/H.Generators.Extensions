using System.Collections.Immutable;
using System.Globalization;
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
                    id: diagnostic.Id,
                    category: diagnostic.Descriptor.Category,
                    message: diagnostic.GetMessage(CultureInfo.InvariantCulture),
                    severity: diagnostic.Severity,
                    defaultSeverity: diagnostic.DefaultSeverity,
                    isEnabledByDefault: diagnostic.Descriptor.IsEnabledByDefault,
                    warningLevel: diagnostic.WarningLevel,
                    title: diagnostic.Descriptor.Title,
                    description: diagnostic.Descriptor.Description,
                    isSuppressed: diagnostic.IsSuppressed,
                    helpLink: diagnostic.Descriptor.HelpLinkUri,
                    location: Location.Create(
                        filePath: diagnostic.Location.GetLineSpan().Path.Replace('\\', '/'),
                        textSpan: diagnostic.Location.SourceSpan,
                        lineSpan: diagnostic.Location.GetLineSpan().Span),
                    additionalLocations: diagnostic.AdditionalLocations,
                    properties: diagnostic.Properties,
                    customTags: diagnostic.Descriptor.CustomTags)
                : diagnostic)
            .ToImmutableArray();
    }
}
