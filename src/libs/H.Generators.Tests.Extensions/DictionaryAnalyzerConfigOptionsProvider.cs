using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace H.Generators.IntegrationTests;

/// <summary>
/// 
/// </summary>
public class DictionaryAnalyzerConfigOptionsProvider : AnalyzerConfigOptionsProvider
{
    /// <summary>
    /// 
    /// </summary>
    public override AnalyzerConfigOptions GlobalOptions { get; }

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, Dictionary<string, string>> TreeOptions { get; }

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, Dictionary<string, string>> AdditionalTextOptions { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="globalOptions"></param>
    /// <param name="treeOptions"></param>
    /// <param name="additionalTextOptions"></param>
    public DictionaryAnalyzerConfigOptionsProvider(
        Dictionary<string, string>? globalOptions = null,
        Dictionary<string, Dictionary<string, string>>? treeOptions = null,
        Dictionary<string, Dictionary<string, string>>? additionalTextOptions = null)
    {
        GlobalOptions = new DictionaryAnalyzerConfigOptions(globalOptions ?? new Dictionary<string, string>());
        TreeOptions = treeOptions ?? new Dictionary<string, Dictionary<string, string>>();
        AdditionalTextOptions = additionalTextOptions ?? new Dictionary<string, Dictionary<string, string>>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tree"></param>
    /// <returns></returns>
    public override AnalyzerConfigOptions GetOptions(SyntaxTree tree)
    {
        tree = tree ?? throw new ArgumentNullException(nameof(tree));

        return new DictionaryAnalyzerConfigOptions(TreeOptions[tree.FilePath]);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="textFile"></param>
    /// <returns></returns>
    public override AnalyzerConfigOptions GetOptions(AdditionalText textFile)
    {
        textFile = textFile ?? throw new ArgumentNullException(nameof(textFile));

        return new DictionaryAnalyzerConfigOptions(AdditionalTextOptions[textFile.Path]);
    }
}
