using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace H.Generators.Extensions;

public static class SourceProductionContextExtensions
{
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

    public static void ReportException(
        this SourceProductionContext context,
        string id,
        Exception exception)
    {
        id = id ?? throw new ArgumentNullException(nameof(id));
        exception = exception ?? throw new ArgumentNullException(nameof(exception));

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
