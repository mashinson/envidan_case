namespace SOLID.Assignment.Reporting;

public static class ShapeMessagesHelper
{
    public static string Area(string shape, string area) => $"The area of the {shape} is {area}";
    public static string Largest(string shape) => $"The {shape} is the largest";
    public static string Tied(IEnumerable<string> shapes) => $"The {string.Join(", ", shapes)} are tied for the largest";
    public static string AllSame(IEnumerable<string> shapes) => $"The {string.Join(", ", shapes)} are all the same size";
}
