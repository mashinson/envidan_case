using SOLID.Assignment.Interfaces;

namespace SOLID.Assignment.Shapes;

public class RightAngledTriangle : IShape
{
    public RightAngledTriangle(int width, int height)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);
        Area = width * height / 2.0;
    }

    public double Area { get; }

    public string Name => "triangle";
}
