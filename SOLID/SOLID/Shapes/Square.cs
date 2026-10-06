using SOLID.Assignment.Interfaces;

namespace SOLID.Assignment.Shapes;

public class Square : IShape
{
    public Square(int side)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(side);
        Area = (double)side * side;
    }

    public double Area { get; }

    public string Name => "square";
}
