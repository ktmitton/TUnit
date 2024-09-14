using TUnit.Assertions.AssertionBuilders;

namespace TUnit.Assertions.AssertConditions.Operators;

public class ValueOr<TActual> 
    : Or<TActual, ValueAnd<TActual>, ValueOr<TActual>>, IValueAssertions<TActual, ValueAnd<TActual>, ValueOr<TActual>>, IOr<TActual, ValueAnd<TActual>, ValueOr<TActual>>
{
    public ValueOr(BaseAssertCondition<TActual, ValueAnd<TActual>, ValueOr<TActual>> otherAssertCondition) : base(otherAssertCondition)
    {
    }

    private AssertionBuilderConnector<TActual, ValueAnd<TActual>, ValueOr<TActual>> AssertionBuilderConnector => new(OtherAssertCondition.AssertionBuilder, ConnectorType.And, OtherAssertCondition);

    public AssertionBuilder<TActual, ValueAnd<TActual>, ValueOr<TActual>> AssertionBuilder =>
        AssertionBuilderConnector.AssertionBuilder;
    
    public static ValueOr<TActual> Create(BaseAssertCondition<TActual, ValueAnd<TActual>, ValueOr<TActual>> otherAssertCondition)
    {
        return new ValueOr<TActual>(otherAssertCondition);
    }
}