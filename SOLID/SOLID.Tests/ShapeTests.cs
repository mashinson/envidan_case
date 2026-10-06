using SOLID.Assignment.Shapes;

namespace SOLID.Assignment.Tests;

public class ShapeTests
{
    [Fact]
    public void Circle_Area_IsPiTimesRadiusSquared()
    {
        Assert.Equal(Math.PI * 4, new Circle(2).Area, 9);
    }

    [Fact]
    public void RightAngledTriangle_Area_IsHalfOfWidthTimesHeight()
    {
        Assert.Equal(6.0, new RightAngledTriangle(3, 4).Area);
    }

    [Fact]
    public void Square_Area_IsSideSquared()
    {
        Assert.Equal(25.0, new Square(5).Area);
    }

    [Fact]
    public void LargeSizes_DoNotOverflow()
    {
        // 100 000 * 100 000 does not fit in an int, so this checks the calculation is done in double
        Assert.Equal(1e10, new Square(100_000).Area);
        Assert.Equal(5e9, new RightAngledTriangle(100_000, 100_000).Area);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void InvalidSize_Throws(int size)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Circle(size));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Square(size));
        Assert.Throws<ArgumentOutOfRangeException>(() => new RightAngledTriangle(size, 1));
        Assert.Throws<ArgumentOutOfRangeException>(() => new RightAngledTriangle(1, size));
    }
}
