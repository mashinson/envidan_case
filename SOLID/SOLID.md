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

## Assumptions

The brief leaves a few things open, so I decided them myself:

- All shapes are compared, not just two. When several share the largest area, the output says they are "tied for the largest", or "all the same size" if every shape has the same area.
- Sizes are still random between 1 and 10, as in the original. A size of 0 or less is rejected.
- Areas are printed with 2 decimals and always with a dot as the decimal separator, regardless of the machine's language settings.
- `GetArea()` is now an `Area` property, calculated once in the constructor, since a shape's size never changes.
- The shape records are now classes. They only expose `Area` and `Name`, check their sizes in the constructor and don't need value equality.
- `Name` (for example "circle") is display text kept on the shape. That's fine for a small console app; in a bigger one I would map shapes to display text outside the model.

## How I applied SOLID

### S – Single Responsibility
*A class should have only one reason to change.*

Each class has one job. Shapes calculate their area, the factory creates shapes, the comparer finds the largest one(s), the report decides what the result means and builds the text, and the writer decides where the text goes.

### O – Open/Closed
*Add new behaviour with new code instead of changing existing code.*

The comparer only uses `IShape.Area` and works with a list of any size. Adding `Square` took one new class and two changes in the factory: a `CreateRandomSquareShape()` method (in `IRandomShapeCreation` and `RandomShapeCreation`) and a new entry in the shape list. The comparer, report and writer didn't change. The factory is the only part that knows the concrete shape types, so it's the only place that changes when a shape is added.

### L – Liskov Substitution
*A child type must be able to replace its parent without breaking anything.*

Here the "parent" is the `IShape` interface. Every shape works wherever an `IShape` is expected, and nothing checks the concrete type. Shapes reject sizes of 0 or less in the constructor, so an invalid shape can't exist. `IShape` only has members every shape can implement (`Area` and `Name`), so no shape has to throw `NotImplementedException`.

### I – Interface Segregation
*A class shouldn't have to implement methods it doesn't use, so keep interfaces small.*

`IShape` has only `Area` and `Name`, and `IOutputWriter` has only `WriteResult`. Reporting and writing are separate interfaces, so the output can move from the console to a file without touching the report. `IRandomShapeCreation` has one method per shape, so a caller can ask for one specific random shape. `Program.cs` only uses `CreateRandomShapeList()`.

### D – Dependency Inversion
*High-level code depends on abstractions, not concrete classes.*

Classes depend on interfaces. `ShapeReport` receives an `IOutputWriter` through its constructor, and `RandomShapeCreation` receives its `Random`. `Program.cs` is the only place that decides which implementation each interface gets. That keeps the parts testable: the comparer and the report are tested with fixed shapes and a fake writer, and the factory can be tested with a seeded `Random`.
