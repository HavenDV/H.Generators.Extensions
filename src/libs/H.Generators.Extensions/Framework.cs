namespace H.Generators.Extensions;

/// <summary>
/// Framework enumeration for generators that are platform specific.
/// </summary>
public enum Framework
{
    /// <summary>
    /// Not recognized.
    /// </summary>
    None,

    /// <summary>
    /// Windows Presentation Foundation.
    /// </summary>
    Wpf,

    /// <summary>
    /// Universal Windows Platform.
    /// </summary>
    Uwp,

    /// <summary>
    /// WinUI 3/WindowsAppSDK.
    /// </summary>
    WinUi,

    /// <summary>
    /// Uno.
    /// </summary>
    Uno,

    /// <summary>
    /// Uno WinUI.
    /// </summary>
    UnoWinUi,

    /// <summary>
    /// AvaloniaUI.
    /// </summary>
    Avalonia,

    /// <summary>
    /// MAUI.
    /// </summary>
    Maui,
}
