string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/5");
}

string input = File.ReadAllText(inputFilePath);

input = input.Replace("\r", "");

SeedRangesExtractor seedExtractor = new(input);
MapExtractor mapExtractor = new(input);
ClosestLocationFinder closestLocationFinder = new (seedExtractor.SeedRanges, mapExtractor.SourceToDestinationMaps);

Console.WriteLine($"The closest location at which a seed can be planted is {closestLocationFinder.ClosestLocation}");