string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file input.txt not found in current directory.");
}

string input = File.ReadAllText(inputFilePath);

SeedExtractor seedExtractor = new(input);
MapExtractor mapExtractor = new(input);
SeedLocator seedLocator = new(mapExtractor.SourceToDestinationMaps);

long closestLocation = long.MaxValue;

foreach(long seed in seedExtractor.Seeds)
{
  long location = seedLocator.GetLocation(seed);
  if(location < closestLocation) closestLocation = location;
}

Console.WriteLine($"The closest location at which a seed can be planted is {closestLocation}");