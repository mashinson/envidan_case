using SOLID.Assignment.Interfaces;
using SOLID.Assignment.Reporting;
using SOLID.Assignment.ShapeComparers;
using SOLID.Assignment.ShapeCreation;
using SOLID.Assignment.ShapeWriter;

// Composition root: the only place that chooses which implementation is used for each interface
IOutputWriter writer = new ConsoleWriter();
IRandomShapeCreation creationFactory = new RandomShapeCreation(Random.Shared);
IShapeAreaComparer comparer = new ShapeAreaComparer();
IShapeReport report = new ShapeReport(writer);

var shapes = creationFactory.CreateRandomShapeList();
report.ReportShapeArea(shapes);

var largestShapes = comparer.FindLargest(shapes);
report.ReportLargestShape(largestShapes, shapes);
