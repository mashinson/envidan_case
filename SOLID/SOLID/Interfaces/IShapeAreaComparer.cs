namespace SOLID.Assignment.Interfaces;

public interface IShapeAreaComparer
{
    List<IShape> FindLargest(IReadOnlyList<IShape> shapes);
}
