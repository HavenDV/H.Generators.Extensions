using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace H.Generators.Extensions;

public static class AnalyzerConfigOptionsProviderExtensions
{
    public static string? GetGlobalOption(
        this AnalyzerConfigOptionsProvider provider,
        string name,
        string? prefix = null)
    {
        provider = provider ?? throw new ArgumentNullException(nameof(provider));
        name = name ?? throw new ArgumentNullException(nameof(name));

        if (prefix != null)
        {
            name = $"{prefix}_{name}";
        }

        return provider.GlobalOptions.TryGetValue(
            $"build_property.{name}",
            out var result) &&
            !string.IsNullOrWhiteSpace(result)
            ? result
            : null;
    }

    public static string? GetOption(
        this AnalyzerConfigOptionsProvider provider,
        AdditionalText text,
        string name,
        string? prefix = null)
    {
        provider = provider ?? throw new ArgumentNullException(nameof(provider));
        name = name ?? throw new ArgumentNullException(nameof(name));

        if (prefix != null)
        {
            name = $"{prefix}_{name}";
        }

        return provider.GetOptions(text).TryGetValue(
            $"build_metadata.AdditionalFiles.{name}",
            out var result) &&
            !string.IsNullOrWhiteSpace(result)
            ? result
            : null;
    }

    public static string GetRequiredGlobalOption(
        this AnalyzerConfigOptionsProvider provider,
        string name,
        string? prefix = null)
    {
        return
            provider.GetGlobalOption(name, prefix) ??
            throw new InvalidOperationException($"{name} is required.");
    }

    public static string GetRequiredOption(
        this AnalyzerConfigOptionsProvider provider,
        AdditionalText text,
        string name,
        string? prefix = null)
    {
        return
            provider.GetOption(text, name, prefix) ??
            throw new InvalidOperationException($"{name} is required.");
    }
}
