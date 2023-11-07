using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Testing;

namespace H.Generators.Tests.Extensions;

/// <summary>
/// 
/// </summary>
public static class FrameworkReferenceAssemblies
{
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70Uwp =
        new(() =>
            LatestReferenceAssemblies.Net70Windows.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Microsoft.NETCore.UniversalWindowsPlatform", "6.2.14"),
                    new PackageIdentity("Microsoft.UI.Xaml", "2.8.6"),
                    new PackageIdentity("Microsoft.Net.UWPCoreRuntimeSdk", "2.2.14"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet80Uwp =
        new(() =>
            LatestReferenceAssemblies.Net80Windows.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Microsoft.NETCore.UniversalWindowsPlatform", "6.2.14"),
                    new PackageIdentity("Microsoft.UI.Xaml", "2.8.6"),
                    new PackageIdentity("Microsoft.Net.UWPCoreRuntimeSdk", "2.2.14"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70WinUi =
        new(() =>
            LatestReferenceAssemblies.Net70Windows.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Microsoft.WindowsAppSDK", "1.4.231008000"),
                    new PackageIdentity("Microsoft.UI.Xaml", "2.8.6"),
                    new PackageIdentity("Microsoft.Windows.SDK.NET.Ref", "10.0.22621.31"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet80WinUi =
        new(() =>
            LatestReferenceAssemblies.Net80Windows.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Microsoft.WindowsAppSDK", "1.4.231008000"),
                    new PackageIdentity("Microsoft.UI.Xaml", "2.8.6"),
                    new PackageIdentity("Microsoft.Windows.SDK.NET.Ref", "10.0.22621.31"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70Maui =
        new(() =>
            LatestReferenceAssemblies.Net70.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Microsoft.Maui.Controls.Ref.any", "7.0.101"),
                    new PackageIdentity("Microsoft.Maui.Core.Ref.any", "7.0.101"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet80Maui =
        new(() =>
            LatestReferenceAssemblies.Net80.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Microsoft.Maui.Controls.Ref.any", "7.0.101"),
                    new PackageIdentity("Microsoft.Maui.Core.Ref.any", "7.0.101"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet60Avalonia =
        new(() =>
            ReferenceAssemblies.Net.Net60.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Avalonia", "11.0.5"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70Avalonia =
        new(() =>
            LatestReferenceAssemblies.Net70.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Avalonia", "11.0.5"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet80Avalonia =
        new(() =>
            LatestReferenceAssemblies.Net80.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Avalonia", "11.0.5"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70Uno4 =
        new(() =>
            LatestReferenceAssemblies.Net70.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Uno.UI", "4.10.26"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70Uno4WinUi =
        new(() =>
            LatestReferenceAssemblies.Net70.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Uno.WinUI", "4.10.26"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70Uno =
        new(() =>
            LatestReferenceAssemblies.Net70.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Uno.UI", "5.0.31"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70UnoWinUi =
        new(() =>
            LatestReferenceAssemblies.Net70.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Uno.WinUI", "5.0.31"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet80Uno4 =
        new(() =>
            LatestReferenceAssemblies.Net80.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Uno.UI", "4.10.26"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet80Uno4WinUi =
        new(() =>
            LatestReferenceAssemblies.Net80.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Uno.WinUI", "4.10.26"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet80Uno =
        new(() =>
            LatestReferenceAssemblies.Net80.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Uno.UI", "5.0.31"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet80UnoWinUi =
        new(() =>
            LatestReferenceAssemblies.Net80.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Uno.WinUI", "5.0.31"))));
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70Uwp => _lazyNet70Uwp.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net80Uwp => _lazyNet80Uwp.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70WinUi => _lazyNet70WinUi.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net80WinUi => _lazyNet80WinUi.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70Maui => _lazyNet70Maui.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net80Maui => _lazyNet80Maui.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net60Avalonia => _lazyNet60Avalonia.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70Avalonia => _lazyNet70Avalonia.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net80Avalonia => _lazyNet80Avalonia.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70Uno4 => _lazyNet70Uno4.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70Uno4WinUi => _lazyNet70Uno4WinUi.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70Uno => _lazyNet70Uno.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70UnoWinUi => _lazyNet70UnoWinUi.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net80Uno4 => _lazyNet80Uno4.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net80Uno4WinUi => _lazyNet80Uno4WinUi.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net80Uno => _lazyNet80Uno.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net80UnoWinUi => _lazyNet80UnoWinUi.Value;
}
