﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace H.Generators.Extensions;

/// <summary>
/// 
/// </summary>
public static class AnalyzerConfigOptionsProviderExtensions
{
    private static string GetFullName(string name, string? prefix = null)
    {
        return prefix == null
            ? name
            : $"{prefix}_{name}";
    }

    /// <summary>
    /// Returns the value of the global option, or null if the option is missing or an empty string.
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="name"></param>
    /// <param name="prefix"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string? GetGlobalOption(
        this AnalyzerConfigOptionsProvider provider,
        string name,
        string? prefix = null)
    {
        provider = provider ?? throw new ArgumentNullException(nameof(provider));
        name = name ?? throw new ArgumentNullException(nameof(name));


        return provider.GlobalOptions.TryGetValue(
            $"build_property.{GetFullName(name, prefix)}",
            out var result) &&
            !string.IsNullOrWhiteSpace(result)
            ? result
            : null;
    }

    /// <summary>
    /// Returns the value of the <see cref="AdditionalText"/> option, or null if the option is missing or an empty string.
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="text"></param>
    /// <param name="name"></param>
    /// <param name="prefix"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string? GetOption(
        this AnalyzerConfigOptionsProvider provider,
        AdditionalText text,
        string name,
        string? prefix = null)
    {
        provider = provider ?? throw new ArgumentNullException(nameof(provider));
        name = name ?? throw new ArgumentNullException(nameof(name));

        return provider.GetOptions(text).TryGetValue(
            $"build_metadata.AdditionalFiles.{GetFullName(name, prefix)}",
            out var result) &&
            !string.IsNullOrWhiteSpace(result)
            ? result
            : null;
    }

    /// <summary>
    /// Returns the value of the global option, or throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="name"></param>
    /// <param name="prefix"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static string GetRequiredGlobalOption(
        this AnalyzerConfigOptionsProvider provider,
        string name,
        string? prefix = null)
    {
        return
            provider.GetGlobalOption(name, prefix) ??
            throw new InvalidOperationException($"{GetFullName(name, prefix)} MSBuild property is required.");
    }

    /// <summary>
    /// Returns the value of the <see cref="AdditionalText"/> option, or throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="text"></param>
    /// <param name="name"></param>
    /// <param name="prefix"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static string GetRequiredOption(
        this AnalyzerConfigOptionsProvider provider,
        AdditionalText text,
        string name,
        string? prefix = null)
    {
        return
            provider.GetOption(text, name, prefix) ??
            throw new InvalidOperationException($"{GetFullName(name, prefix)} metadata for AdditionalText is required.");
    }
}
