using TUnit.Assertions.AssertionBuilders;

namespace TUnit.Assertions.AssertConditions.Operators;

public class ValueAnd<TActual> 
    : And<TActual, ValueAnd<TActual>, ValueOr<TActual>>, IValueAssertions<TActual, ValueAnd<TActual>, ValueOr<TActual>>, IAnd<TActual, ValueAnd<TActual>, ValueOr<TActual>>
{
    public ValueAnd(BaseAssertCondition<TActual, ValueAnd<TActual>, ValueOr<TActual>> otherAssertCondition) : base(otherAssertCondition)
    {
    }

    private AssertionBuilderConnector<TActual, ValueAnd<TActual>, ValueOr<TActual>> AssertionBuilderConnector => new(OtherAssertCondition.AssertionBuilder, ConnectorType.And, OtherAssertCondition);

    public static ValueAnd<TActual> Create(BaseAssertCondition<TActual, ValueAnd<TActual>, ValueOr<TActual>> otherAssertCondition)
    {
        return new ValueAnd<TActual>(otherAssertCondition);
    }
    
    public AssertionBuilder<TActual, ValueAnd<TActual>, ValueOr<TActual>> AssertionBuilder =>
        AssertionBuilderConnector.AssertionBuilder;
}