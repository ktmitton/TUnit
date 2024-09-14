using TUnit.Assertions.AssertionBuilders;

namespace TUnit.Assertions.AssertConditions.Operators;

public class DelegateAnd<TActual> 
    : And<TActual, DelegateAnd<TActual>, DelegateOr<TActual>>, IDelegateAssertions<TActual, DelegateAnd<TActual>, DelegateOr<TActual>>, IAnd<TActual, DelegateAnd<TActual>, DelegateOr<TActual>>
{
    public DelegateAnd(BaseAssertCondition<TActual, DelegateAnd<TActual>, DelegateOr<TActual>> otherAssertCondition) : base(otherAssertCondition)
    {
    }

    private AssertionBuilderConnector<TActual, DelegateAnd<TActual>, DelegateOr<TActual>> AssertionBuilderConnector => new(OtherAssertCondition.AssertionBuilder, ConnectorType.And, OtherAssertCondition);

    public static DelegateAnd<TActual> Create(BaseAssertCondition<TActual, DelegateAnd<TActual>, DelegateOr<TActual>> otherAssertCondition)
    {
        return new DelegateAnd<TActual>(otherAssertCondition);
    }

    public AssertionBuilder<TActual, DelegateAnd<TActual>, DelegateOr<TActual>> AssertionBuilder =>
        AssertionBuilderConnector.AssertionBuilder;
}