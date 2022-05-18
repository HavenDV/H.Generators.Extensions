using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace H.Generators.Extensions;

public static class AnalyzerConfigOptionsProviderExtensions
{
    public static string? GetGlobalOption(
        this AnalyzerConfigOptionsProvider provider,
        string name)
    {
        provider = provider ?? throw new ArgumentNullException(nameof(provider));
        name = name ?? throw new ArgumentNullException(nameof(name));

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
        string name)
    {
        provider = provider ?? throw new ArgumentNullException(nameof(provider));
        name = name ?? throw new ArgumentNullException(nameof(name));

        return provider.GetOptions(text).TryGetValue(
            $"build_metadata.AdditionalFiles.{name}",
            out var result) &&
            !string.IsNullOrWhiteSpace(result)
            ? result
            : null;
    }

    public static string GetRequiredGlobalOption(
        this AnalyzerConfigOptionsProvider provider,
        string name)
    {
        return
            provider.GetGlobalOption(name) ??
            throw new InvalidOperationException($"{name} is required.");
    }

    public static string GetRequiredOption(
        this AnalyzerConfigOptionsProvider provider,
        AdditionalText text,
        string name)
    {
        return
            provider.GetOption(text, name) ??
            throw new InvalidOperationException($"{name} is required.");
    }
}
