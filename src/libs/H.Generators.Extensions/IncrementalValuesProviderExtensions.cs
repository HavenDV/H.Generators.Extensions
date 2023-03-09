using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
// ReSharper disable MemberCanBePrivate.Global

namespace H.Generators;

/// <summary>
/// 
/// </summary>
public static class IncrementalValuesProviderExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="source"></param>
    public static void AddSource(
        this IncrementalValuesProvider<FileWithName> source,
        IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(source, static (context, file) =>
        {
            if (file.IsEmpty)
            {
                return;
            }

            context.AddSource(
                hintName: file.Name,
                source: file.Text);
        });
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="source"></param>
    public static void AddSource(
        this IncrementalValueProvider<FileWithName> source,
        IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(source, static (context, file) =>
        {
            if (file.IsEmpty)
            {
                return;
            }
            
            context.AddSource(
                hintName: file.Name,
                source: file.Text);
        });
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="initializationContext"></param>
    /// <param name="id"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static IncrementalValuesProvider<TResult> SelectAndReportExceptions<TSource, TResult>(
        this IncrementalValuesProvider<TSource> source,
        Func<TSource, TResult> selector,
        IncrementalGeneratorInitializationContext initializationContext,
        string id = "SRE001")
    {
        var outputWithErrors = source
            .Select<TSource, (TResult? Value, Exception? Exception)>((value, cancellationToken) =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                
                try
                {
                    return (Value: selector(value), Exception: null);
                }
                catch (Exception exception)
                {
                    return (Value: default, Exception: exception);
                }
            });

        initializationContext.RegisterSourceOutput(outputWithErrors
            .Where(static x => x.Exception is not null),
            (context, tuple) =>
            {
                context.ReportException(id: id, exception: tuple.Exception!);
            });
        
        return outputWithErrors
            .Where(static x => x.Exception is null)
            .Select(static (x, _) => x.Value!);
    }

    /// <summary>
    /// Specific case after Combine with detect framework.
    /// Filters nullable values and select non-nullable values.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="context"></param>
    /// <param name="id"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TLeft"></typeparam>
    /// <returns></returns>
    public static IncrementalValuesProvider<TResult>
        SelectAndReportExceptions<TResult, TLeft>(
            this IncrementalValuesProvider<(TLeft Left, Framework Right)> source,
            Func<Framework, TLeft, TResult?> selector,
            IncrementalGeneratorInitializationContext context,
            string id) where TResult : struct
    {
        return source
            .SelectAndReportExceptions(x => selector(x.Right, x.Left), context, id)
            .Where(static x => x is not null)
            .Select(static (x, _) => x!.Value);
    }
    
    /// <summary>
    /// Returns <see cref="Framework.None"/> if the framework is not recognized and report diagnostic.
    /// </summary>
    /// <param name="initializationContext"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IncrementalValueProvider<Framework> DetectFramework(
        this IncrementalGeneratorInitializationContext initializationContext,
        string name)
    {
        var frameworkWithDiagnostic = initializationContext.AnalyzerConfigOptionsProvider
            .Select<AnalyzerConfigOptionsProvider, (Framework Framework, Diagnostic? Diagnostic)>((options, _) =>
            {
                var framework = options.TryRecognizeFramework(prefix: name);

                var diagnostic = framework == Framework.None
                    ? Diagnostic.Create(
                        new DiagnosticDescriptor(
                            id: "TRF001",
                            title: "Framework is not recognized",
                            messageFormat: @"Framework is not recognized.
You can explicitly specify the framework by setting one of the following constants in your project:
HAS_WPF, HAS_WINUI, HAS_UWP, HAS_UNO, HAS_UNO_WINUI, HAS_AVALONIA, HAS_MAUI",
                            "Usage",
                            DiagnosticSeverity.Error,
                            true),
                        Location.None)
                    : null;
                
                return (Framework: framework, Diagnostic: diagnostic);
            });
        
        initializationContext.RegisterSourceOutput(
            frameworkWithDiagnostic,
            (context, tuple) =>
            {
                if (tuple.Diagnostic == null)
                {
                    return;
                }
                
                context.ReportDiagnostic(tuple.Diagnostic);
            });
        
        return frameworkWithDiagnostic
            .Select(static (x, _) => x.Framework);
    }
}
