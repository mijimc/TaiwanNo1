using FluentAssertions;
using Xunit;

namespace TaiwanNo1.Validation.Tests
{
    public class StringExtensions_IsTwTaxIdVaildShould
    {
        [Theory]
        [InlineData("22099131")]
        [InlineData("16003518")]
        [InlineData("96979933")]
        [InlineData("20828393")]
        [InlineData("23638777")]
        [InlineData("12694272")]
        public void IsTwTaxIdVaild_InputDifferentTaxIds_ReturnTrue(string taxId)
        {
            taxId.IsTwTaxIdVaild().Should().BeTrue();
        }
    }
}
