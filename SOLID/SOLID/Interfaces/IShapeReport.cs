namespace SOLID.Assignment.Interfaces;

public interface IShapeReport
{
    void ReportShapeArea(IReadOnlyList<IShape> shapes);
    void ReportLargestShape(IReadOnlyList<IShape> largestShapes, IReadOnlyList<IShape> allShapes);
}
