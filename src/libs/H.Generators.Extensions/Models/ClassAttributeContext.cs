using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace H.Generators.Extensions;

/// <summary>
/// 
/// </summary>
/// <param name="SemanticModel"></param>
/// <param name="Attributes"></param>
/// <param name="ClassSyntax"></param>
/// <param name="ClassSymbol"></param>
public readonly record struct ClassWithAttributesContext(
    SemanticModel SemanticModel,
    ImmutableArray<AttributeData> Attributes,
    ClassDeclarationSyntax ClassSyntax,
    INamedTypeSymbol ClassSymbol);
