---
sidebar_position: 1
---

# Installing TUnit

First create an empty .NET console application:

```powershell
dotnet new console --name YourTestProjectNameHere
```

To that project add the `TUnit` package:

```powershell
cd YourTestProjectNameHere
dotnet add package TUnit --prerelease
```

And then remove any automatically generated `Program.cs` or main method, as this'll be taken care of by the TUnit package.

That's it. We're ready to write our first test.

Your `.csproj` should be as simple as something like:

```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net8.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
    </PropertyGroup>

    <ItemGroup>
      <PackageReference Include="TUnit" VersionOverride="$(TUnitVersion)" />
    </ItemGroup>

</Project>
```

Any test functions that do not access instance data from their containing class will trigger [CA1822](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1822) warnings by the Roslyn analyzers. To suppress these warning, you can add `<NoWarn>CA1822</NoWarn>` to the PropertyGroup section of your `.csproj` file:

```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net8.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
        <!-- Suppress Analyzer Warning -->
        <NoWarn>CA1822</NoWarn>
    </PropertyGroup>

    <ItemGroup>
      <PackageReference Include="TUnit" VersionOverride="$(TUnitVersion)" />
    </ItemGroup>

</Project>
```

## Incompatibilities

If you're used to other testing frameworks, you're probably used to the package `Microsoft.NET.Test.Sdk`.
This should NOT be used with TUnit. It'll stop test discovery from working properly.

## .NET Framework
If you are still targeting .NET Framework, you may have compilation errors around missing types, such as the `ModuleInitialiserAttribute`.
These are looked at by the compiler, not the runtime, so you can define them yourselves or use a polyfill library. Using a Polyfill library is the easiest, for example, just reference the NuGet package `Polyfill`.
