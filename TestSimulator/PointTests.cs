using Simulator;
using Xunit;

namespace TestSimulator;

public class PointTests
{
    [Fact]
    public void ToString_ShouldReturnCorrectFormat()
    {
        var point = new Point(3, 4);
        Assert.Equal("(3, 4)", point.ToString());
    }

    [Theory]
    [InlineData(0, 0, Direction.Up, 0, -1)]
    [InlineData(0, 0, Direction.Right, 1, 0)]
    [InlineData(5, 5, Direction.Down, 5, 6)]
    [InlineData(5, 5, Direction.Left, 4, 5)]
    public void Next_ShouldReturnCorrectPoint(int x, int y, Direction direction, int expectedX, int expectedY)
    {
        var point = new Point(x, y);
        var nextPoint = point.Next(direction);
        Assert.Equal(new Point(expectedX, expectedY), nextPoint);
    }

    [Theory]
    [InlineData(0, 0, Direction.Up, 1, -1)]
    [InlineData(0, 0, Direction.Right, 1, 1)]
    [InlineData(5, 5, Direction.Down, 4, 6)]
    [InlineData(5, 5, Direction.Left, 4, 4)]
    public void NextDiagonal_ShouldReturnCorrectPoint(int x, int y, Direction direction, int expectedX, int expectedY)
    {
        var point = new Point(x, y);
        var nextDiagonal = point.NextDiagonal(direction);
        Assert.Equal(new Point(expectedX, expectedY), nextDiagonal);
    }
}
