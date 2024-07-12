﻿using Microsoft.Testing.Platform.CommandLine;
using TUnit.Core;
using TUnit.Engine.CommandLineProviders;

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).

namespace TUnit.Engine.Logging;

internal class StandardErrorConsoleInterceptor : ConsoleInterceptor
{
    private readonly ICommandLineOptions _commandLineOptions;
    public static StandardErrorConsoleInterceptor Instance { get; private set; } = null!;

    private readonly TUnitLogger _logger;

    public static TextWriter DefaultError { get; }
    
    public override StringWriter? OutputWriter => TestContext.Current?.ErrorWriter;
    
    static StandardErrorConsoleInterceptor()
    {
        DefaultError = Console.Error;
    }

    public StandardErrorConsoleInterceptor(TUnitLogger logger, ICommandLineOptions commandLineOptions)
    {
        _commandLineOptions = commandLineOptions;
        _logger = logger;
        Instance = this;
    }
    
    public override void Initialize()
    {
        Console.SetError(this);
    }

    public override void SetModule(TestContext testContext)
    {
        testContext.OnDispose = async (_, _) =>
        {
            try
            {
                if (_commandLineOptions.IsOptionSet(DisplayTestOutputCommandProvider.DisplayTestOutput))
                {
                    await _logger.LogInformationAsync(testContext.GetConsoleErrorOutput());
                }
            }
            catch (Exception e)
            {
                await _logger.LogErrorAsync(e);
            }
        };
    }

    private protected override void ResetDefault()
    {
        Console.SetError(DefaultError);
    }
}