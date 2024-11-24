﻿namespace TUnit.Core.SourceGenerator;

public record StaticClassDataSourceInjectorModel
{
    public required string FullyQualifiedTypeName { get; init; }
    public required string PropertyName { get; init; }
    public required string InjectableType { get; init; }
    public required string MinimalTypeName { get; set; }
}