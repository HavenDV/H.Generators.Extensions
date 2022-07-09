using Microsoft.CodeAnalysis;
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

    /// <summary>
    /// Returns true if generator running in design-time.
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static bool IsDesignTime(this AnalyzerConfigOptionsProvider provider)
    {
        var isBuildingProjectValue = provider.GetGlobalOption("BuildingProject"); // legacy projects
        var isDesignTimeBuildValue = provider.GetGlobalOption("DesignTimeBuild"); // sdk-style projects

        return string.Equals(isBuildingProjectValue, "false", StringComparison.OrdinalIgnoreCase)
            || string.Equals(isDesignTimeBuildValue, "true", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Try recognize the platform using MSBuild properties and constants.
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="prefix">Prefix for your MSBuild DefineConstants property</param>
    /// <returns></returns>
    public static Platform? TryRecognizePlatform(this AnalyzerConfigOptionsProvider provider, string prefix)
    {
        provider = provider ?? throw new ArgumentNullException(nameof(provider));
        prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));

        var constants = provider.GetGlobalOption("DefineConstants", prefix: prefix) ?? string.Empty;
        var useWpf = bool.Parse(provider.GetGlobalOption("UseWPF") ?? bool.FalseString) || constants.Contains("HAS_WPF");
        var useWinUI = bool.Parse(provider.GetGlobalOption("UseWinUI") ?? bool.FalseString) || constants.Contains("HAS_WINUI");
        var useMaui = bool.Parse(provider.GetGlobalOption("UseMaui") ?? bool.FalseString) || constants.Contains("HAS_MAUI");
        var useUwp = constants.Contains("WINDOWS_UWP") || constants.Contains("HAS_UWP");
        var useUno = constants.Contains("HAS_UNO");
        var useUnoWinUI = constants.Contains("HAS_UNO_WINUI") || (constants.Contains("HAS_UNO") && constants.Contains("HAS_WINUI"));
        var useAvalonia = constants.Contains("HAS_AVALONIA");

        return (useWpf, useUwp, useWinUI, useUno, useUnoWinUI, useAvalonia, useMaui) switch
        {
            (_, _, _, _, _, _, true) => Platform.MAUI,
            (_, _, _, _, _, true, _) => Platform.Avalonia,
            (_, _, _, _, true, _, _) => Platform.UnoWinUI,
            (_, _, _, true, _, _, _) => Platform.Uno,
            (_, _, true, _, _, _, _) => Platform.WinUI,
            (_, true, _, _, _, _, _) => Platform.UWP,
            (true, _, _, _, _, _, _) => Platform.WPF,
            _ => null,
        };
    }

    /// <summary>
    /// Recognizes the platform using MSBuild properties and constants or throws an exception.
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="prefix"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static Platform RecognizePlatform(this AnalyzerConfigOptionsProvider provider, string prefix)
    {
        return
            provider.TryRecognizePlatform(prefix) ??
            throw new InvalidOperationException(@"Platform is not recognized.
You can explicitly specify the platform by setting one of the following constants in your project:
HAS_WPF, HAS_WINUI, HAS_UWP, HAS_UNO, HAS_UNO_WINUI, HAS_AVALONIA, HAS_MAUI");
    }
}
