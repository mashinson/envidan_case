using System.Globalization;
using SOLID.Assignment.Interfaces;

namespace SOLID.Assignment.Reporting;

public class ShapeReport(IOutputWriter writer) : IShapeReport
{
    public void ReportShapeArea(IReadOnlyList<IShape> shapes)
    {
        foreach (var shape in shapes)
        {
            writer.WriteResult(ShapeMessagesHelper.Area(shape.Name, shape.Area.ToString("F2", CultureInfo.InvariantCulture)));
        }
    }

    public void ReportLargestShape(IReadOnlyList<IShape> largestShapes, IReadOnlyList<IShape> allShapes)
    {
        var names = largestShapes.Select(s => s.Name);

        if (largestShapes.Count == 1)
        {
            writer.WriteResult(ShapeMessagesHelper.Largest(largestShapes[0].Name));
        }
        else if (largestShapes.Count == allShapes.Count)
        {
            writer.WriteResult(ShapeMessagesHelper.AllSame(names));
        }
        else
        {
            writer.WriteResult(ShapeMessagesHelper.Tied(names));
        }
    }
}
