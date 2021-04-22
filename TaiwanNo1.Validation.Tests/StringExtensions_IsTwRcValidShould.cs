using FluentAssertions;
using Xunit;

namespace TaiwanNo1.Validation.Tests
{
    public class StringExtensions_IsTwRcValidShould
    {
        [Theory]
        [InlineData("A800000005")]
        [InlineData("A900000007")]
        public void IsTwRcValid_InputDifferentGenderIds_ReturnTrue(string id)
        {
            id.IsTwRcValid(false).Should().BeTrue();
        }

        [Theory]
        [InlineData("A800000005")]
        [InlineData("A900000007")]
        [InlineData("AA00000009")]
        [InlineData("AB00000001")]
        [InlineData("AC00000003")]
        [InlineData("AD00000005")]
        public void IsTwRcValid_OldFormat_InputDifferentGenderIds_ReturnTrue(string id)
        {
            id.IsTwRcValid(true).Should().BeTrue();
        }
    }
}