using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
// ReSharper disable MemberCanBePrivate.Global

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
    /// <param name="options"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string? GetOption(
        this AnalyzerConfigOptions options,
        string key)
    {
        options = options ?? throw new ArgumentNullException(nameof(options));
        key = key ?? throw new ArgumentNullException(nameof(key));

        return
            options.TryGetValue(key, out var result) &&
            !string.IsNullOrWhiteSpace(result)
                ? result
                : null;
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

        return provider.GlobalOptions.GetOption($"build_property.{GetFullName(name, prefix)}");
    }

    /// <summary>
    /// Returns the value of the <see cref="AdditionalText"/> option, or null if the option is missing or an empty string.
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="text"></param>
    /// <param name="name"></param>
    /// <param name="group">Default: AdditionalFiles</param>
    /// <param name="prefix"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string? GetOption(
        this AnalyzerConfigOptionsProvider provider,
        AdditionalText text,
        string name,
        string? group = null,
        string? prefix = null)
    {
        provider = provider ?? throw new ArgumentNullException(nameof(provider));
        name = name ?? throw new ArgumentNullException(nameof(name));
        group ??= "AdditionalFiles";

        return provider.GetOptions(text).GetOption($"build_metadata.{group}.{GetFullName(name, prefix)}");
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
    /// Try recognize the framework using MSBuild properties and constants.
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static Framework TryRecognizeFramework(this AnalyzerConfigOptionsProvider provider)
    {
        provider = provider ?? throw new ArgumentNullException(nameof(provider));

        var constants = provider.GetGlobalOption("DefineConstants", prefix: "RecognizeFramework") ?? string.Empty;
        var useWpf = bool.Parse(provider.GetGlobalOption("UseWPF") ?? bool.FalseString) || constants.Contains("HAS_WPF");
        var useWinUi = bool.Parse(provider.GetGlobalOption("UseWinUI") ?? bool.FalseString) || constants.Contains("HAS_WINUI");
        var useMaui = bool.Parse(provider.GetGlobalOption("UseMaui") ?? bool.FalseString) || constants.Contains("HAS_MAUI");
        var useUwp = constants.Contains("WINDOWS_UWP") || constants.Contains("HAS_UWP");
        var useUno = constants.Contains("HAS_UNO");
        var useUnoWinUi = constants.Contains("HAS_UNO_WINUI") || (constants.Contains("HAS_UNO") && constants.Contains("HAS_WINUI"));
        var useAvalonia = constants.Contains("HAS_AVALONIA");

        return (useWpf, useUwp, useWinUi, useUno, useUnoWinUi, useAvalonia, useMaui) switch
        {
            (_, _, _, _, _, _, true) => Framework.Maui,
            (_, _, _, _, _, true, _) => Framework.Avalonia,
            (_, _, _, _, true, _, _) => Framework.UnoWinUi,
            (_, _, _, true, _, _, _) => Framework.Uno,
            (_, _, true, _, _, _, _) => Framework.WinUi,
            (_, true, _, _, _, _, _) => Framework.Uwp,
            (true, _, _, _, _, _, _) => Framework.Wpf,
            _ => Framework.None,
        };
    }

    /// <summary>
    /// 
    /// </summary>
    public const string FrameworkIsNotRecognized = @"Framework is not recognized.
You can explicitly specify the framework by setting one of the following constants in your project:
HAS_WPF, HAS_WINUI, HAS_UWP, HAS_UNO, HAS_UNO_WINUI, HAS_AVALONIA, HAS_MAUI";

    /// <summary>
    /// Recognizes the framework using MSBuild properties and constants or throws an exception.
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static Framework RecognizeFramework(this AnalyzerConfigOptionsProvider provider)
    {
        var framework = provider.TryRecognizeFramework();
        if (framework != Framework.None)
        {
            return framework;
        }

        throw new InvalidOperationException(FrameworkIsNotRecognized);
    }
}
