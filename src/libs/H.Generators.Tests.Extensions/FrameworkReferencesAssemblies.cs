using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Testing;

namespace H.Generators.Tests.Extensions;

/// <summary>
/// 
/// </summary>
public static class FrameworkReferencesAssemblies
{
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70Uwp =
        new(() =>
            LatestReferencesAssemblies.Net70Windows.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Microsoft.NETCore.UniversalWindowsPlatform", "6.2.14"),
                    new PackageIdentity("Microsoft.UI.Xaml", "2.8.5"),
                    new PackageIdentity("Microsoft.Net.UWPCoreRuntimeSdk", "2.2.14"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70WinUi =
        new(() =>
            LatestReferencesAssemblies.Net70Windows.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Microsoft.WindowsAppSDK", "1.4.230822000"),
                    new PackageIdentity("Microsoft.UI.Xaml", "2.8.5"),
                    new PackageIdentity("Microsoft.Windows.SDK.NET.Ref", "10.0.22621.29"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70Maui =
        new(() =>
            LatestReferencesAssemblies.Net70.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Microsoft.Maui.Controls.Ref.any", "7.0.92"),
                    new PackageIdentity("Microsoft.Maui.Core.Ref.any", "7.0.92"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70Avalonia =
        new(() =>
            LatestReferencesAssemblies.Net70.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Avalonia", "11.0.4"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70Uno =
        new(() =>
            LatestReferencesAssemblies.Net70.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Uno.UI", "4.9.45"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70UnoWinUi =
        new(() =>
            LatestReferencesAssemblies.Net70.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Uno.WinUI", "4.9.45"))));
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70Uwp => _lazyNet70Uwp.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70WinUi => _lazyNet70WinUi.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70Maui => _lazyNet70Maui.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70Avalonia => _lazyNet70Avalonia.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70Uno => _lazyNet70Uno.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70UnoWinUi => _lazyNet70UnoWinUi.Value;
}
