using Simulator;
using Simulator.Maps;
using Xunit;

namespace TestSimulator;

public class SmallSquareMapTests
{
    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(20)]
    public void Constructor_ValidSize_ShouldSetSize(int size)
    {
        var map = new SmallSquareMap(size);
        Assert.Equal(size, map.Size);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(21)]
    public void Constructor_InvalidSize_ShouldThrowArgumentOutOfRangeException(int size)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new SmallSquareMap(size));
    }

    [Theory]
    [InlineData(3, 3, 5, true)]
    [InlineData(5, 5, 5, false)]
    [InlineData(19, 19, 20, true)]
    [InlineData(-1, 0, 10, false)]
    public void Exist_ShouldReturnCorrectValue(int x, int y, int size, bool expected)
    {
        var map = new SmallSquareMap(size);
        var point = new Point(x, y);
        Assert.Equal(expected, map.Exist(point));
    }

    [Theory]
    [InlineData(0, 0, Direction.Up, 0, 0)]
    [InlineData(19, 19, Direction.Down, 19, 19)]
    [InlineData(5, 5, Direction.Right, 6, 5)]
    [InlineData(5, 5, Direction.Left, 4, 5)]
    public void Next_ShouldHandleEdgesCorrectly(int x, int y, Direction direction, int expectedX, int expectedY)
    {
        var map = new SmallSquareMap(20);
        var nextPoint = map.Next(new Point(x, y), direction);
        Assert.Equal(new Point(expectedX, expectedY), nextPoint);
    }
}