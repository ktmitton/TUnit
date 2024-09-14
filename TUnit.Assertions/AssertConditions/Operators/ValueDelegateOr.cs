using TUnit.Assertions.AssertionBuilders;

namespace TUnit.Assertions.AssertConditions.Operators;

public class ValueDelegateOr<TActual> 
    : Or<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>>, IValueAssertions<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>>, IDelegateAssertions<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>>, IOr<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>>
 {
    public ValueDelegateOr(BaseAssertCondition<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>> otherAssertCondition) : base(otherAssertCondition)
    {
    }
    
    private AssertionBuilderConnector<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>> AssertionBuilderConnector => new(OtherAssertCondition.AssertionBuilder, ConnectorType.And, OtherAssertCondition);
    public AssertionBuilder<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>> AssertionBuilder =>
        AssertionBuilderConnector.AssertionBuilder;
    
    public static ValueDelegateOr<TActual> Create(BaseAssertCondition<TActual, ValueDelegateAnd<TActual>, ValueDelegateOr<TActual>> otherAssertCondition)
    {
        return new ValueDelegateOr<TActual>(otherAssertCondition);
    }
}