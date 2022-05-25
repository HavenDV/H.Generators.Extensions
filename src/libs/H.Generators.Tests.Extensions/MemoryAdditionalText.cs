using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace H.Generators.IntegrationTests;

/// <summary>
/// 
/// </summary>
public class MemoryAdditionalText : AdditionalText
{
    /// <summary>
    /// 
    /// </summary>
    private string Text { get; }

    /// <summary>
    /// 
    /// </summary>
    public override string Path { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="text"></param>
    public MemoryAdditionalText(string path, string text)
    {
        Path = path;
        Text = text;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override SourceText GetText(CancellationToken cancellationToken = default)
    {
        return SourceText.From(Text);
    }
}
