string[] input = File.ReadAllLines("input.txt");

List<(Point Position, Speed Speed)> positionAndSpeedList = InputParser.GetPointAndSpeedListFromInput(input);

PathIntersectionDetector pathIntersectionDetector = new(positionAndSpeedList);

Console.WriteLine($"The number of hailstones whose paths cross within the search area is: {pathIntersectionDetector.PathIntersections}");