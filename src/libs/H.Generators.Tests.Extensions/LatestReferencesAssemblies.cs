using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Testing;

namespace H.Generators.Tests.Extensions;

/// <summary>
/// 
/// </summary>
public static class LatestReferencesAssemblies
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
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70 => _lazyNet70.Value;
    
    /// <summary>
    /// 
    /// </summary>
    public static ReferenceAssemblies Net70Windows => _lazyNet70Windows.Value;
}
