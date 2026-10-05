using SOLID.Assignment.Interfaces;
using SOLID.Assignment.Reporting;
using SOLID.Assignment.ShapeComparers;
using SOLID.Assignment.ShapeCreation;
using SOLID.Assignment.ShapeWriter;

// Composition root: the only place that knows the concrete classes
IOutputWriter writer = new ConsoleWriter();
IRandomShapeCreation creationFactory = new RandomShapeCreation();
IShapeAreaComparer comparer = new ShapeAreaComparer();
IShapeReport report = new ShapeReport(writer);

var shapes = creationFactory.CreateRandomShapeList();
report.ReportShapeArea(shapes);

var largestShapes = comparer.FindLargest(shapes);
report.ReportLargestShape(largestShapes, shapes);
