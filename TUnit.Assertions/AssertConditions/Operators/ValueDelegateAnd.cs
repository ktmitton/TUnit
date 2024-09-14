using TUnit.Assertions.AssertionBuilders;

namespace TUnit.Assertions.AssertConditions.Operators;

public class ValueDelegateAnd<TActual> 
    : And<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>>, IValueAssertions<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>>, IDelegateAssertions<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>>, IAnd<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>>
{
    public ValueDelegateAnd(BaseAssertCondition<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>> otherAssertCondition) : base(otherAssertCondition)
    {
    }
    
    public static ValueDelegateAnd<TActual> Create(BaseAssertCondition<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>> otherAssertCondition)
    {
        return new ValueDelegateAnd<TActual>(otherAssertCondition);
    }

    private AssertionBuilderConnector<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>> AssertionBuilderConnector => new(OtherAssertCondition.AssertionBuilder, ConnectorType.And, OtherAssertCondition);
    public AssertionBuilder<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>> AssertionBuilder =>
        AssertionBuilderConnector.AssertionBuilder;
}