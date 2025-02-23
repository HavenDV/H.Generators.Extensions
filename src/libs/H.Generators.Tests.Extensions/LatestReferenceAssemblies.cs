using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Testing;

namespace H.Generators.Tests.Extensions;

/// <summary>
/// 
/// </summary>
public static class LatestReferenceAssemblies
{
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70 =
        new(() => new ReferenceAssemblies(
            "net7.0",
            new PackageIdentity(
                "Microsoft.NETCore.App.Ref",
                "7.0.0"),
            Path.Combine("ref", "net7.0")));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet70Windows =
        new(() =>
            Net70.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Microsoft.WindowsDesktop.App.Ref", "7.0.10"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet80 =
        new(() => new ReferenceAssemblies(
            "net8.0",
            new PackageIdentity(
                "Microsoft.NETCore.App.Ref",
                "8.0.11"),
            Path.Combine("ref", "net8.0")));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet80Windows =
        new(() =>
            Net80.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Microsoft.WindowsDesktop.App.Ref", "8.0.11"))));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet90 =
        new(() => new ReferenceAssemblies(
            "net9.0",
            new PackageIdentity(
                "Microsoft.NETCore.App.Ref",
                "9.0.0"),
            Path.Combine("ref", "net9.0")));
    
    private static readonly Lazy<ReferenceAssemblies> _lazyNet90Windows =
        new(() =>
            Net80.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("Microsoft.WindowsDesktop.App.Ref", "9.0.0"))));
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70 => _lazyNet70.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70Windows => _lazyNet70Windows.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net80 => _lazyNet80.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net80Windows => _lazyNet80Windows.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net90 => _lazyNet90.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net90Windows => _lazyNet90Windows.Value;
}
