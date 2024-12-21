using Simulator;
using Xunit;

namespace TestSimulator;

public class ValidatorTests
{
    [Theory]
    [InlineData(5, 1, 10, 5)]
    [InlineData(0, 1, 10, 1)]
    [InlineData(15, 1, 10, 10)]
    public void Limiter_ShouldClampValue(int value, int min, int max, int expected)
    {
        Assert.Equal(expected, Validator.Limiter(value, min, max));
    }

    [Theory]
    [InlineData("Test", 3, 10, '#', "Test")]
    [InlineData("Short", 6, 10, '#', "Short#")]
    [InlineData("VeryLongString", 3, 10, '#', "VeryLongSt")]
    public void Shortener_ShouldReturnCorrectString(string value, int min, int max, char placeholder, string expected)
    {
        Assert.Equal(expected, Validator.Shortener(value, min, max, placeholder));
    }

    [Fact]
    public void Shortener_NullInput_ShouldReturnDefaultString()
    {
        Assert.Equal("###", Validator.Shortener(null, 3, 10, '#'));
    }
}
