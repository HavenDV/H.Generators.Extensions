using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace H.Generators.Extensions;

/// <summary>
/// 
/// </summary>
public static class SourceProductionContextExtensions
{
    /// <summary>
    /// Adds <see cref="SourceText"/> in <see cref="Encoding.UTF8"/> encoding.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="hintName"></param>
    /// <param name="text"></param>
    /// <param name="encoding"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void AddTextSource(
        this SourceProductionContext context,
        string hintName,
        string text,
        Encoding? encoding = null)
    {
        hintName = hintName ?? throw new ArgumentNullException(nameof(hintName));
        text = text ?? throw new ArgumentNullException(nameof(text));

        context.AddSource(
            hintName,
            SourceText.From(
                text,
                encoding ?? Encoding.UTF8));
    }

    /// <summary>
    /// Generates a diagnostic for the selected exception.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="id"></param>
    /// <param name="exception"></param>
    /// <param name="prefix"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void ReportException(
        this SourceProductionContext context,
        string id,
        Exception exception,
        string? prefix = null)
    {
        id = id ?? throw new ArgumentNullException(nameof(id));
        exception = exception ?? throw new ArgumentNullException(nameof(exception));

        if (prefix != null)
        {
            id = $"{prefix}{id}";
        }

        context.ReportDiagnostic(
            Diagnostic.Create(
                new DiagnosticDescriptor(
                    id,
                    "Exception: ",
                    $"{exception}",
                    "Usage",
                    DiagnosticSeverity.Error,
                    true),
                Location.None));
    }
}
