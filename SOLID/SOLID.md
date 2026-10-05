# Assignment 1 – SOLID refactor and Square support

## What I changed

The original `Program.cs` did everything in one place: it created the shapes, calculated their areas, compared exactly two of them with `if / else` and printed the result. I split it into small classes behind interfaces and added a `Square`.

| Folder | Classes | Job |
|---|---|---|
| `Shapes` | `Circle`, `RightAngledTriangle`, `Square` | Each shape calculates its own `Area` and has a `Name` |
| `ShapeCreation` | `RandomShapeCreation` | Creates shapes with random sizes |
| `ShapeComparers` | `ShapeAreaComparer` | Finds the largest shape(s) |
| `ShapeReport` | `ShapeReport`, `ShapeMessagesHelper` | Picks the message (one largest, a tie, or all the same) and builds the text |
| `ShapeWriter` | `ConsoleWriter` | Writes the text to the console |
| `Program.cs` | | Wires everything together |

## How I applied SOLID

### S – Single Responsibility
*A class should have only one reason to change.*

Each class has one job. Shapes calculate their area, the factory creates shapes, the comparer finds the largest one(s), the report decides what the result means and builds the text, and the writer decides where the text goes.

### O – Open/Closed
*Add new behaviour with new code instead of changing existing code.*

The comparer only uses `IShape.Area` and works with a list of any size. Adding `Square` took one new class and one new entry in the factory (`RandomShapeCreation`). The comparer, report and writer didn't change. No code depends on a specific shape type, so another shape would also only touch the factory.

### L – Liskov Substitution
*A child type must be able to replace its parent without breaking anything.*

Here the "parent" is the `IShape` interface. Every shape works wherever an `IShape` is expected, and nothing checks the concrete type. Shapes reject sizes of 0 or less in the constructor, so an invalid shape can't exist. `IShape` only has members every shape can implement (`Area` and `Name`), so no shape has to throw `NotImplementedException`.

### I – Interface Segregation
*A class shouldn't have to implement methods it doesn't use, so keep interfaces small.*

`IShape` has only `Area` and `Name`, and `IOutputWriter` has only `WriteResult`. Reporting and writing are separate interfaces, so the output can move from the console to a file without touching the report.

### D – Dependency Inversion
*High-level code depends on abstractions, not concrete classes.*

Classes depend on interfaces. For example, `ShapeReport` receives an `IOutputWriter` through its constructor. `Program.cs` is the only place that creates concrete classes. Because the comparer works with any list of `IShape`, it can be unit-tested with fixed shapes instead of random ones.
