﻿namespace TUnit.Core.SourceGenerator.Arguments;

public abstract record ArgumentsContainer(ArgumentsType ArgumentsType) : DataAttributeContainer(ArgumentsType)
{
    public required bool DisposeAfterTest { get; init; }

    protected string VariableNamePrefix
    {
        get
        {
            return ArgumentsType switch
            {
                ArgumentsType.ClassConstructor => VariableNames.ClassArg,
                ArgumentsType.Property => VariableNames.PropertyArg,
                _ => VariableNames.MethodArg
            };
        }
    }
    
    protected string DataAttributeVariableNamePrefix
    {
        get
        {
            return ArgumentsType switch
            {
                ArgumentsType.ClassConstructor => VariableNames.ClassDataAttribute,
                ArgumentsType.Property => VariableNames.PropertyDataAttribute,
                _ => VariableNames.MethodDataAttribute
            };
        }
    }

    protected Variable GenerateVariable(string type, string value, ref int globalIndex)
    {
        if (globalIndex == 0)
        {
            var generateVariable = AddVariable(new Variable
            {
                Type = type,
                Name = VariableNamePrefix,
                Value = value
            });
            
            globalIndex++;
            
            return generateVariable;
        }

        return AddVariable(new Variable
        {
            Type = type,
            Name = $"{VariableNamePrefix}{globalIndex++}",
            Value = value
        });
    }
    
    protected Variable GenerateDataAttributeVariable(string type, string value, ref int globalIndex)
    {
        if (globalIndex == 0)
        {
            globalIndex++;
            return AddDataAttributeVariable(new Variable
            {
                Type = type,
                Name = DataAttributeVariableNamePrefix,
                Value = value
            });
        }

        return AddDataAttributeVariable(new Variable
        {
            Type = type,
            Name = $"{DataAttributeVariableNamePrefix}{globalIndex}",
            Value = value
        });
    }

    protected Variable AddVariable(Variable variable)
    {
        DataVariables.Add(variable);
        return variable;
    }
    
    protected Variable AddDataAttributeVariable(Variable variable)
    {
        DataAttributesVariables.Add(variable);
        return variable;
    }
};