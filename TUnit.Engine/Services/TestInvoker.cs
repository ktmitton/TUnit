﻿using TUnit.Core;
using TUnit.Core.Interfaces;
using TUnit.Engine.Helpers;
using TUnit.Engine.Hooks;

namespace TUnit.Engine.Services;

internal class TestInvoker
{
    public async Task Invoke(DiscoveredTest discoveredTest, CancellationToken cancellationToken)
    {
        if (discoveredTest.TestDetails.ClassInstance is IAsyncInitializer asyncInitializer)
        {
            await asyncInitializer.InitializeAsync();
        }
        
        await TestHookOrchestrator.ExecuteBeforeHooks(discoveredTest.TestContext.TestDetails.ClassInstance!, discoveredTest);
            
        await Timings.Record("Main Test Body", discoveredTest.TestContext, () => discoveredTest.ExecuteTest(cancellationToken));
    }
}