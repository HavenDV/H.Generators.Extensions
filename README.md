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
- `options.TryRecognizePlatform(string prefix)`
- `options.RecognizePlatform(string prefix)`
To recognize the platform, you will need to add the following code to your %PackageId%.props(In this case, the passed prefix will be equal to %PackageId%):
```xml
<Project>

  <ItemGroup>
    <CompilerVisibleProperty Include="%PackageId%_DefineConstants"/>
    <CompilerVisibleProperty Include="UseWPF"/>
    <CompilerVisibleProperty Include="UseWinUI"/>
    <CompilerVisibleProperty Include="UseMaui"/>
  </ItemGroup>

  <Target Name="CreateDefineConstants" BeforeTargets="GenerateMSBuildEditorConfigFileShouldRun;GenerateMSBuildEditorConfigFileCore">

    <PropertyGroup>
      <%PackageId%_DefineConstants>$(DefineConstants.Replace(';',','))</%PackageId%_DefineConstants>
    </PropertyGroup>

  </Target>
  
</Project>
```

### SourceProductionContextExtensions 
- `context.AddTextSource(string hintName, string text)`
- `context.ReportException(string id, Exception)`

### StringExtensions 
- `name.ToPropertyName()`
- `name.ToParameterName()`
- `text.RemoveBlankLinesWhereOnlyWhitespaces()`
- `fullTypeName.ExtractNamespace()`
- `fullTypeName.ExtractSimpleName()`
- `fullTypeName.WithGlobalPrefix()`

### EnumerableExtensions 
- `values.Inject()`

# H.Generators.Tests.Extensions
- `DictionaryAnalyzerConfigOptions`
- `DictionaryAnalyzerConfigOptionsProvider`
- `MemoryAdditionalText`

## Support
You can get answers to your questions in my discord support channel:  
https://discord.gg/g8u2t9dKgE