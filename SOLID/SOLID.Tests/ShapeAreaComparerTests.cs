using SOLID.Assignment.Interfaces;
using SOLID.Assignment.ShapeComparers;
using SOLID.Assignment.Shapes;

namespace SOLID.Assignment.Tests;

public class ShapeAreaComparerTests
{
    private readonly ShapeAreaComparer _comparer = new();

    [Fact]
    public void OneShapeIsBiggest_ReturnsOnlyThatShape()
    {
        var square = new Square(3);                     // area 9
        var triangle = new RightAngledTriangle(2, 4);   // area 4

        var largest = _comparer.FindLargest([square, triangle]);

        Assert.Equal(new IShape[] { square }, largest);
    }

    [Fact]
    public void BiggestShapeIsLast_ReturnsOnlyThatShape()
    {
        var circle = new Circle(1);                     // area ~3.14
        var square = new Square(3);                     // area 9

        var largest = _comparer.FindLargest([circle, square]);

        Assert.Equal(new IShape[] { square }, largest);
    }

    [Fact]
    public void TwoShapesShareBiggestArea_ReturnsBoth()
    {
        var square = new Square(2);                     // area 4
        var triangle = new RightAngledTriangle(2, 4);   // area 4
        var circle = new Circle(1);                     // area ~3.14

        var largest = _comparer.FindLargest([square, triangle, circle]);

        Assert.Equal(new IShape[] { square, triangle }, largest);
    }

    [Fact]
    public void SingleShape_ReturnsThatShape()
    {
        var circle = new Circle(5);

        var largest = _comparer.FindLargest([circle]);

        Assert.Equal(new IShape[] { circle }, largest);
    }

    [Fact]
    public void EmptyList_Throws()
    {
        Assert.Throws<ArgumentException>(() => _comparer.FindLargest([]));
    }

    [Fact]
    public void NullList_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => _comparer.FindLargest(null!));
    }
}
