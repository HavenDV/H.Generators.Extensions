# H.Generators.Extensions
A set of extensions to simplify the code of generators.
In addition to extensions, this library also adds a .props file to automatically add your generation-time dependencies 
to the NuGet package and Integration tests.

## Install
The usage is pretty simple:
```xml
<PackageReference Include="H.Generators.Extensions" Version="1.4.2" PrivateAssets="all" />
```
I want to note that PrivateAssets="all" is required to rule out some issues.

## Extensions
### AnalyzerConfigOptionsProviderExtensions 
- `options.GetGlobalOption(string name)`
- `options.GetOption(AdditionalText, string name)`
- `options.GetRequiredGlobalOption(string name)`
- `options.GetRequiredOption(AdditionalText, string name)`
- `options.TryRecognizeFramework()`
- `options.RecognizeFramework()`
To recognize the framework, you will need to add the following code to your %PackageId%.props:
```xml
<Project>

  <ItemGroup>
    <CompilerVisibleProperty Include="RecognizeFramework_DefineConstants"/>
    <CompilerVisibleProperty Include="UseWPF"/>
    <CompilerVisibleProperty Include="UseWinUI"/>
    <CompilerVisibleProperty Include="UseMaui"/>
  </ItemGroup>

  <Target Name="CreateDefineConstants" BeforeTargets="GenerateMSBuildEditorConfigFileShouldRun;GenerateMSBuildEditorConfigFileCore">

    <PropertyGroup>
      <RecognizeFramework_DefineConstants>$(DefineConstants.Replace(';',','))</RecognizeFramework_DefineConstants>
    </PropertyGroup>

  </Target>
  
</Project>
```

### SourceProductionContextExtensions 
- `context.ReportException(string id, Exception)`

### StringExtensions 
- `name.ToPropertyName()`
- `name.ToParameterName()`
- `text.RemoveBlankLinesWhereOnlyWhitespaces()`
- `text.NormalizeLineEndings(string? newLine = null)`
- `fullTypeName.ExtractNamespace()`
- `fullTypeName.ExtractSimpleName()`
- `fullTypeName.WithGlobalPrefix()`

### EnumerableExtensions 
- `values.Inject()`

# H.Generators.Tests.Extensions
- `DictionaryAnalyzerConfigOptions`
- `DictionaryAnalyzerConfigOptionsProvider`
- `MemoryAdditionalText`
- `ImmutableArrayExtensions.NormalizeLocations`

## Support
You can get answers to your questions in my discord support channel:  
https://discord.gg/g8u2t9dKgE