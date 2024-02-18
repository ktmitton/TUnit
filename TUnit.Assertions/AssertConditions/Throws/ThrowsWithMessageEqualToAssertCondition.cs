using TUnit.Assertions.AssertConditions.Operators;
using TUnit.Assertions.AssertionBuilders;

namespace TUnit.Assertions.AssertConditions.Throws;

public class ThrowsWithMessageEqualToAssertCondition<TActual, TAnd, TOr> : AssertCondition<TActual, string, TAnd, TOr>
    where TAnd : And<TActual, TAnd, TOr>, IAnd<TAnd, TActual, TAnd, TOr>
    where TOr : Or<TActual, TAnd, TOr>, IOr<TOr, TActual, TAnd, TOr>
{
    private readonly StringComparison _stringComparison;
    private readonly Func<Exception?, Exception?> _exceptionSelector;

    public ThrowsWithMessageEqualToAssertCondition(AssertionBuilder<TActual> assertionBuilder, string expected,
        StringComparison stringComparison, Func<Exception?, Exception?> exceptionSelector) : base(assertionBuilder, expected)
    {
        _stringComparison = stringComparison;
        _exceptionSelector = exceptionSelector;
    }
    
    protected override string DefaultMessage => $"Message was {_exceptionSelector(Exception)?.Message} instead of {ExpectedValue}";

    protected internal override bool Passes(TActual? actualValue, Exception? rootException)
    {
        var exception = _exceptionSelector(rootException);

        if (exception is null)
        {
            WithMessage((_, _) => "Exception is null");
            return false;
        }
        
        return string.Equals(exception.Message, ExpectedValue, _stringComparison);
    }
}