using SOLID.Assignment.Interfaces;

namespace SOLID.Assignment.Shapes;

public class Circle : IShape
{
    public Circle(int radius)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(radius);
        Area = Math.PI * radius * radius;
    }

    public double Area { get; }

    public string Name => "circle";
}
