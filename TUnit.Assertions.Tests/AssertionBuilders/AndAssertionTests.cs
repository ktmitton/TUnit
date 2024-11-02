﻿namespace TUnit.Assertions.Tests.AssertionBuilders;

public sealed class AndAssertionTests
{
    [Test]
    public async Task Throws_When_Mixed_With_Or()
    {
        char[] sut = "ABCD".ToCharArray();
#pragma warning disable TUnitAssertions0001
        var action = async () => await Assert.That(sut)
            .Contains('A').Or
            .Contains('B').Or
            .Contains('C').And
            .Contains('D');
#pragma warning restore TUnitAssertions0001

        await Assert.That(action).Throws<MixedAndOrAssertionsException>();
    }

    [Test]
    public async Task Does_Not_Throw_For_Multiple_And()
    {
        char[] sut = "ABCD".ToCharArray();
#pragma warning disable TUnitAssertions0001
        var action = async () => await Assert.That(sut)
            .Contains('A').And
            .Contains('B').And
            .Contains('C').And
            .Contains('D');
#pragma warning restore TUnitAssertions0001

        await Assert.That(action).ThrowsNothing();
    }
}