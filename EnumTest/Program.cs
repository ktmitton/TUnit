using EnumDefintion;
using FluentAssertions;
using TUnit.Core;

namespace EnumTest;

public class EnumTest
{
    [Test]
    [Arguments(MyEnum.Foo)]
    [Arguments(MyEnum.Bar)]
    public void MyEnumTest(MyEnum myEnum)
    {
        myEnum.Should().Be(MyEnum.Foo);
    }
}