using SOLID.Assignment.Interfaces;

namespace SOLID.Assignment.ShapeWriter;

public class ConsoleWriter : IOutputWriter
{
    public void WriteResult(string message) => Console.WriteLine(message);
}
