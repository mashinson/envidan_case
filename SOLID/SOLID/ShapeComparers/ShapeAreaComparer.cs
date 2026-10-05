using SOLID.Assignment.Interfaces;

namespace SOLID.Assignment.ShapeComparers;

public class ShapeAreaComparer : IShapeAreaComparer
{
    // Areas are doubles, so compare with a small tolerance instead of ==
    private const double Tolerance = 1e-9;

    public List<IShape> FindLargest(IReadOnlyList<IShape> shapes)
    {
        EnsureNotEmpty(shapes);

        var largest = new List<IShape>();
        var maxArea = double.MinValue;

        foreach (var shape in shapes)
        {
            if (shape.Area > maxArea + Tolerance)
            {
                // New biggest shape: forget the previous ones
                maxArea = shape.Area;
                largest.Clear();
                largest.Add(shape);
            }
            else if (Math.Abs(shape.Area - maxArea) < Tolerance)
            {
                // Same size as the current biggest: it's a tie
                largest.Add(shape);
            }
        }

        return largest;
    }

    private static void EnsureNotEmpty(IReadOnlyList<IShape> shapes)
    {
        ArgumentNullException.ThrowIfNull(shapes);

        if (shapes.Count == 0)
        {
            throw new ArgumentException("At least one shape is required to compare.", nameof(shapes));
        }
    }
}
