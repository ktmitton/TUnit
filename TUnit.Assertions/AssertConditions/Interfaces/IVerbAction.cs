using TUnit.Assertions.AssertConditions.Operators;
using TUnit.Assertions.AssertionBuilders;

namespace TUnit.Assertions.AssertConditions.Interfaces;

public interface IVerbAction<TActual, TAnd, TOr>
    where TAnd : IAnd<TActual, TAnd, TOr>
    where TOr : IOr<TActual, TAnd, TOr>
{
    internal AssertionBuilder<TActual, TAnd, TOr> AssertionBuilder { get; }
    internal BaseAssertCondition<TActual, TAnd, TOr>? OtherAssertCondition => AssertionBuilder.OtherAssertCondition;
    internal ConnectorType ConnectorType => AssertionBuilder.ConnectorType;
}