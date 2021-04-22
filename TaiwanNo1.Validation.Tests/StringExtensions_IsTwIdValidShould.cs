using Xunit;
using FluentAssertions;

namespace TaiwanNo1.Validation.Tests
{
    public class StringExtensions_IsTwIdValidShould
    {
        [Theory]
        [InlineData("A100000001")]
        [InlineData("B100000002")]
        [InlineData("C100000003")]
        [InlineData("D100000004")]
        [InlineData("E100000005")]
        [InlineData("F100000006")]
        [InlineData("G100000007")]
        [InlineData("H100000008")]
        [InlineData("I100000003")]
        [InlineData("J100000009")]
        [InlineData("K100000000")]
        [InlineData("L100000000")]
        [InlineData("M100000001")]
        [InlineData("N100000002")]
        [InlineData("O100000004")]
        [InlineData("P100000003")]
        [InlineData("Q100000004")]
        [InlineData("R100000005")]
        [InlineData("S100000006")]
        [InlineData("T100000007")]
        [InlineData("U100000008")]
        [InlineData("V100000009")]
        [InlineData("W100000001")]
        [InlineData("X100000009")]
        [InlineData("Y100000000")]
        [InlineData("Z100000002")]
        public void IsTwIdValid_InputDifferentAreaIds_ReturnTrue(string id)
        {
            id.IsTwIdValid().Should().BeTrue();
        }

        [Theory]
        [InlineData("A100000001")]
        [InlineData("A200000003")]
        public void IsTwIdValid_InputDifferentGenderIds_ReturnTrue(string id)
        {
            id.IsTwIdValid().Should().BeTrue();
        }

        [Theory]
        [InlineData("A100000010")]
        [InlineData("A100000109")]
        [InlineData("A100001008")]
        [InlineData("A100010007")]
        [InlineData("A100100006")]
        [InlineData("A101000005")]
        [InlineData("A110000004")]
        [InlineData("A100000403")]
        [InlineData("A100003002")]
        [InlineData("A100000001")]
        public void IsTwIdValid_InputDifferentChecksumIds_ReturnTrue(string id)
        {
            id.IsTwIdValid().Should().BeTrue();
        }
    }
}