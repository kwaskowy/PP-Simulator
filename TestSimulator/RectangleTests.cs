using Simulator;
using Xunit;

namespace TestSimulator;

public class RectangleTests
{
    [Fact]
    public void Constructor_ShouldReorderCoordinates()
    {
        var rect = new Rectangle(10, 20, 5, 15);
        Assert.Equal(5, rect.X1);
        Assert.Equal(15, rect.Y1);
        Assert.Equal(10, rect.X2);
        Assert.Equal(20, rect.Y2);
    }

    [Fact]
    public void Constructor_CollinearCoordinates_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Rectangle(5, 5, 5, 10));
        Assert.Throws<ArgumentException>(() => new Rectangle(5, 5, 10, 5));
    }

    [Theory]
    [InlineData(7, 17, 5, 15, 10, 20, true)]
    [InlineData(4, 14, 5, 15, 10, 20, false)]
    public void Contains_ShouldReturnCorrectValue(int x, int y, int x1, int y1, int x2, int y2, bool expected)
    {
        var rect = new Rectangle(x1, y1, x2, y2);
        var point = new Point(x, y);
        Assert.Equal(expected, rect.Contains(point));
    }
}
