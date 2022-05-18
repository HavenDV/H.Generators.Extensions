# H.Generators.Extensions
A set of extensions to simplify the code of generators.
In addition to extensions, this library also adds a .props file to automatically add your generation-time dependencies to the NuGet package and Integration tests.

### AnalyzerConfigOptionsProviderExtensions 
- `options.GetGlobalOption(string name)`
- `options.GetOption(AdditionalText, string name)`
- `options.GetRequiredGlobalOption(string name)`
- `options.GetRequiredOption(AdditionalText, string name)`

### SourceProductionContextExtensions 
- `context.AddTextSource(string hintName, string text)`
- `context.ReportException(string id, Exception)`