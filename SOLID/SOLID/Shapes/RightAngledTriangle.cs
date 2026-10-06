using SOLID.Assignment.Interfaces;

namespace SOLID.Assignment.Shapes;

public class RightAngledTriangle : IShape
{
    public RightAngledTriangle(int width, int height)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);
        Area = (double)width * height / 2;
    }

    public double Area { get; }

    public string Name => "triangle";
}
