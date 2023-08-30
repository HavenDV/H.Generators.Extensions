using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace H.Generators.Tests.Extensions;

/// <summary>
/// 
/// </summary>
public static class GeneratorDriverExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="driver"></param>
    /// <param name="version"></param>
    /// <param name="compilation"></param>
    /// <param name="outputCompilation"></param>
    /// <param name="diagnostics"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static GeneratorDriver RunGeneratorsAndUpdateCompilation(
        this GeneratorDriver driver,
        LanguageVersion version,
        Compilation compilation,
        out Compilation outputCompilation,
        out ImmutableArray<Diagnostic> diagnostics,
        CancellationToken cancellationToken = default)
    {
        driver = driver ?? throw new ArgumentNullException(nameof(driver));
        compilation = compilation ?? throw new ArgumentNullException(nameof(compilation));
        
        driver = driver.RunGenerators(compilation, cancellationToken);
        var result = driver.GetRunResult();

        outputCompilation = compilation
            .AddSyntaxTrees(result.GeneratedTrees
                .Select(tree => tree.WithRootAndOptions(tree.GetRoot(cancellationToken),
                    CSharpParseOptions.Default.WithLanguageVersion(version))));
        diagnostics = result.Diagnostics;

        return driver;
    }
}
