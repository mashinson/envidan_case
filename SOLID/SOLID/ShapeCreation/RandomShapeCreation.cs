using SOLID.Assignment.Interfaces;
using SOLID.Assignment.Shapes;

namespace SOLID.Assignment.ShapeCreation;

public class RandomShapeCreation(Random random) : IRandomShapeCreation
{
    private const int MaxDimension = 10;

    private int NextDimension() => random.Next(MaxDimension) + 1;

    public IShape CreateRandomCircleShape() => new Circle(NextDimension());

    public IShape CreateRandomRightAngledTriangleShape() => new RightAngledTriangle(NextDimension(), NextDimension());

    public IShape CreateRandomSquareShape() => new Square(NextDimension());

    public IReadOnlyList<IShape> CreateRandomShapeList() =>
    [
        CreateRandomCircleShape(),
        CreateRandomRightAngledTriangleShape(),
        CreateRandomSquareShape()
    ];
}
