using Microsoft.CodeAnalysis;

namespace H.Generators.Extensions;

/// <summary>
/// 
/// </summary>
public static class SourceProductionContextExtensions
{
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
