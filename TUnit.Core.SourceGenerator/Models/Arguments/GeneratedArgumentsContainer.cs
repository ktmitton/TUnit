using Microsoft.CodeAnalysis;
using TUnit.Core.SourceGenerator.CodeGenerators.Writers;
using TUnit.Core.SourceGenerator.Enums;

namespace TUnit.Core.SourceGenerator.Models.Arguments;

public record GeneratedArgumentsContainer : ArgumentsContainer
{
    public GeneratedArgumentsContainer(GeneratorAttributeSyntaxContext context, AttributeData attributeData,
        ArgumentsType ArgumentsType, int AttributeIndex, string TestClassTypeName, string[] GenericArguments,
        string AttributeDataGeneratorType) : base(ArgumentsType)
    {
        this.AttributeIndex = AttributeIndex;
        Context = context;
        AttributeData = attributeData;
        this.TestClassTypeName = TestClassTypeName;
        this.GenericArguments = GenericArguments;
        this.AttributeDataGeneratorType = AttributeDataGeneratorType;
    }
    
    public required string? PropertyName { get; init; }

    public override void OpenScope(SourceCodeWriter sourceCodeWriter, ref int variableIndex)
    {
        if (ArgumentsType is ArgumentsType.Property)
        {
            // No scope as we don't allow enumerables for properties
            return;
        }
        
        var objectToGetAttributesFrom = ArgumentsType switch
        {
            ArgumentsType.Method => "methodInfo",
            ArgumentsType.Property => $"testClassType.GetProperty(\"{PropertyName}\", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)",
            _ => $"typeof({TestClassTypeName})"
        };
        
        var propertyName = "null";
        
        var type = "Parameters";
        
        var parameterInfos = ArgumentsType switch
        {
            ArgumentsType.ClassConstructor => $"{objectToGetAttributesFrom}.GetConstructors().First().GetParameters()",
            _ => $"{objectToGetAttributesFrom}.GetParameters()"
        };

        var dataGeneratorMetadataVariableName = $"{VariableNamePrefix}DataGeneratorMetadata";
        
        sourceCodeWriter.WriteLine($$"""
                                     var {{dataGeneratorMetadataVariableName}} = new DataGeneratorMetadata
                                     {
                                        Type = TUnit.Core.Enums.DataGeneratorType.{{type}},
                                        TestClassType = testClassType,
                                        ParameterInfos = {{parameterInfos}},
                                        PropertyInfo = {{propertyName}},
                                        TestBuilderContext = testBuilderContextAccessor,
                                        TestSessionId = sessionId,
                                     };
                                     """);
        
        var arrayVariableName = $"{VariableNamePrefix}GeneratedDataArray";
        var generatedDataVariableName = $"{VariableNamePrefix}GeneratedData";

        var dataAttr = GenerateDataAttributeVariable("var", AttributeWriter.WriteAttribute(Context, AttributeData),
            ref variableIndex);
            
        sourceCodeWriter.WriteLine(dataAttr.ToString());
        
        sourceCodeWriter.WriteLine();
        
        sourceCodeWriter.WriteLine($"var {arrayVariableName} = {dataAttr.Name}.GenerateDataSources({dataGeneratorMetadataVariableName});");
        sourceCodeWriter.WriteLine();
        sourceCodeWriter.WriteLine($"foreach (var {generatedDataVariableName}Accessor in {arrayVariableName})");
        sourceCodeWriter.WriteLine("{");

        if (ArgumentsType == ArgumentsType.ClassConstructor)
        {
            sourceCodeWriter.WriteLine($"{CodeGenerators.VariableNames.ClassDataIndex}++;");
        }
        
        if (ArgumentsType == ArgumentsType.Method)
        {
            sourceCodeWriter.WriteLine($"{CodeGenerators.VariableNames.TestMethodDataIndex}++;");
        }
    }

    public override void WriteVariableAssignments(SourceCodeWriter sourceCodeWriter, ref int variableIndex)
    {
        var objectToGetAttributesFrom = ArgumentsType switch
        {
            ArgumentsType.Method => "methodInfo",
            ArgumentsType.Property => $"testClassType.GetProperty(\"{PropertyName}\", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)",
            _ => $"typeof({TestClassTypeName})"
        };
        
        var propertyName = "null";
        if (ArgumentsType == ArgumentsType.Property)
        {
            propertyName = $"propertyInfo{variableIndex}";
            sourceCodeWriter.WriteLine($"var {propertyName} = {objectToGetAttributesFrom};");
            objectToGetAttributesFrom = propertyName;
        }
        
        var type = ArgumentsType == ArgumentsType.Property ? "Property" : "Parameters";
        
        var parameterInfos = ArgumentsType switch
        {
            ArgumentsType.Property => "null",
            ArgumentsType.ClassConstructor => $"{objectToGetAttributesFrom}.GetConstructors().First().GetParameters()",
            _ => $"{objectToGetAttributesFrom}.GetParameters()"
        };
        
        if (ArgumentsType == ArgumentsType.Property)
        {
            var attr = GenerateDataAttributeVariable("var",
                AttributeWriter.WriteAttribute(Context, AttributeData),
                ref variableIndex);
            
            sourceCodeWriter.WriteLine(attr.ToString());
            
            sourceCodeWriter.WriteLine(GenerateVariable("var", $$"""
                                                                 {{attr.Name}}.GenerateDataSources(new DataGeneratorMetadata
                                                                 {
                                                                    Type = TUnit.Core.Enums.DataGeneratorType.{{type}},
                                                                    TestClassType = testClassType,
                                                                    ParameterInfos = {{parameterInfos}},
                                                                    PropertyInfo = {{propertyName}},
                                                                    TestBuilderContext = testBuilderContextAccessor,
                                                                    TestSessionId = sessionId,
                                                                 }).ElementAtOrDefault(0)()
                                                                 """, ref variableIndex).ToString());
            sourceCodeWriter.WriteLine();
            return;
        }
        
        var generatedDataVariableName = $"{VariableNamePrefix}GeneratedData";
        
        sourceCodeWriter.WriteLine($"var {generatedDataVariableName} = {generatedDataVariableName}Accessor();");
        
        if (GenericArguments.Length > 1)
        {
            for (var i = 0; i < GenericArguments.Length; i++)
            {
                var refIndex = i;
                sourceCodeWriter.WriteLine(GenerateVariable(GenericArguments[i], $"{generatedDataVariableName}.Item{i + 1}", ref refIndex).ToString());
            }

            sourceCodeWriter.WriteLine();
        }
        else
        {
            DataVariables.Add(new Variable
            {
                Type = "var",
                Name = $"{generatedDataVariableName}",
                Value = string.Empty
            });
        }
    }

    public override void CloseScope(SourceCodeWriter sourceCodeWriter)
    {
        if (ArgumentsType is ArgumentsType.Property)
        {
            return;
        }
        
        sourceCodeWriter.WriteLine("}");
    }
    
    public override string[] GetArgumentTypes()
    {
        return GenericArguments;
    }

    public GeneratorAttributeSyntaxContext Context { get; }
    public AttributeData AttributeData { get; }
    public string TestClassTypeName { get; }

    public string[] GenericArguments { get; }

    public string AttributeDataGeneratorType { get; }
}