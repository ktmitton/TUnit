using TUnit.Assertions.AssertionBuilders;

namespace TUnit.Assertions.AssertConditions.Operators;

public class DelegateOr<TActual> 
    : Or<TActual, DelegateAnd<TActual>, DelegateOr<TActual>>, IDelegateAssertions<TActual, DelegateAnd<TActual>, DelegateOr<TActual>>, IOr<TActual, DelegateAnd<TActual>, DelegateOr<TActual>>
{
    public DelegateOr(BaseAssertCondition<TActual, DelegateAnd<TActual>, DelegateOr<TActual>> otherAssertCondition) : base(otherAssertCondition)
    {
    }

    private AssertionBuilderConnector<TActual, DelegateAnd<TActual>, DelegateOr<TActual>> AssertionBuilderConnector => new(OtherAssertCondition.AssertionBuilder, ConnectorType.And, OtherAssertCondition);
    
    public static DelegateOr<TActual> Create(BaseAssertCondition<TActual, DelegateAnd<TActual>, DelegateOr<TActual>> otherAssertCondition)
    {
        return new DelegateOr<TActual>(otherAssertCondition);
    }
    
    public AssertionBuilder<TActual, DelegateAnd<TActual>, DelegateOr<TActual>> AssertionBuilder =>
        AssertionBuilderConnector.AssertionBuilder;
}