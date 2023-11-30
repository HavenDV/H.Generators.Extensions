using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace H.Generators.Extensions;

/// <summary>
/// 
/// </summary>
/// <param name="Result"></param>
/// <param name="Diagnostics"></param>
/// <typeparam name="T"></typeparam>
public readonly record struct ResultWithDiagnostics<T>(
    T Result,
    EquatableArray<Diagnostic> Diagnostics
)
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    public ResultWithDiagnostics(T result) : this(result, ImmutableArray<Diagnostic>.Empty.AsEquatableArray())
    {
    }
}

/// <summary>
/// 
/// </summary>
public static class ResultWithDiagnosticsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ResultWithDiagnostics<T> ToResultWithDiagnostics<T>(this T result)
    {
        return new ResultWithDiagnostics<T>(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <param name="diagnostics"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ResultWithDiagnostics<T> ToResultWithDiagnostics<T>(this T result, ImmutableArray<Diagnostic> diagnostics)
    {
        return new ResultWithDiagnostics<T>(result, diagnostics.AsEquatableArray());
    }
}
