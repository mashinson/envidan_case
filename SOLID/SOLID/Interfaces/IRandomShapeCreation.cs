namespace SOLID.Assignment.Interfaces;

public interface IRandomShapeCreation
{
    IShape CreateRandomCircleShape();

    IShape CreateRandomRightAngledTriangleShape();

    IShape CreateRandomSquareShape();

    IReadOnlyList<IShape> CreateRandomShapeList();
}
