using SOLID.Assignment.Interfaces;
using SOLID.Assignment.Reporting;
using SOLID.Assignment.Shapes;

namespace SOLID.Assignment.Tests;

public class ShapeReportTests
{
    // Collects messages instead of printing them, so the tests can check the text
    private class FakeWriter : IOutputWriter
    {
        public List<string> Messages { get; } = [];
        public void WriteResult(string message) => Messages.Add(message);
    }

    private readonly FakeWriter _writer = new();
    private readonly ShapeReport _report;

    public ShapeReportTests() => _report = new ShapeReport(_writer);

    [Fact]
    public void OneLargestShape_ReportsLargest()
    {
        var square = new Square(3);
        var triangle = new RightAngledTriangle(2, 4);

        _report.ReportLargestShape([square], [square, triangle]);

        Assert.Equal("The square is the largest", Assert.Single(_writer.Messages));
    }

    [Fact]
    public void SomeShapesShareLargest_ReportsTie()
    {
        var square = new Square(2);
        var triangle = new RightAngledTriangle(2, 4);
        var circle = new Circle(1);

        _report.ReportLargestShape([square, triangle], [square, triangle, circle]);

        Assert.Equal("The square, triangle are tied for the largest", Assert.Single(_writer.Messages));
    }

    [Fact]
    public void AllShapesShareLargest_ReportsAllSame()
    {
        var square = new Square(2);
        var triangle = new RightAngledTriangle(2, 4);

        _report.ReportLargestShape([square, triangle], [square, triangle]);

        Assert.Equal("The square, triangle are all the same size", Assert.Single(_writer.Messages));
    }
}
