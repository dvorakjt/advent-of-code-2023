string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/11");
}

string[] input = File.ReadAllLines(inputFilePath);

for(int i = 0; i < input.Length; i++)
{
  input[i] = input[i].Trim();
}

List<int> emptyRows = LocateEmptyRows(input);
List<int> emptyColumns = LocateEmptyColumns(input);
List<Point> galaxyLocations = LocateGalaxies(input);
List<Point> expandedGalaxyLocations = ExpandGalaxyLocations(galaxyLocations, emptyRows, emptyColumns, 1000000);

long galaxyDistancesSum = SumDistancesBetweenGalaxies(expandedGalaxyLocations);
Console.WriteLine($"The sum of the shortest distances between all pairs of galaxies is {galaxyDistancesSum}");

List<int> LocateEmptyRows(string[] input)
{
  List<int> emptyRows = new();

  for(int i = 0; i < input.Length; i++)
  {
    bool isEmptyRow = true;
    
    for(int j = 0; j < input[i].Length; j++)
    {
      if(input[i][j] == '#') {
        isEmptyRow = false;
        break;
      }
    }

    if(isEmptyRow) emptyRows.Add(i);
  }

  return emptyRows;
}

List<int> LocateEmptyColumns(string[] input)
{
  List<int> emptyColumns = new();

  for(int i = 0; i < input[0].Length; i++)
  {
    bool isEmptyColumn = true;
    
    for(int j = 0; j < input.Length; j++)
    {
      if(input[j][i] == '#'){
        isEmptyColumn = false;
        break;
      } 
    }

    if(isEmptyColumn) emptyColumns.Add(i);
  }

  return emptyColumns;
}

List<Point> LocateGalaxies(string[] input)
{
  List<Point> galaxyLocations = new();

  for(int i = 0; i < input.Length; i++)
  {
    for(int j = 0; j < input[i].Length; j++)
    {
      if(input[i][j] == '#') 
      {
        Point galaxyLocation = new Point(i, j);
        galaxyLocations.Add(galaxyLocation);
      }
    }
  }

  return galaxyLocations;
}

List<Point> ExpandGalaxyLocations(List<Point> galaxyLocations, List<int> emptyRows, List<int> emptyColumns, int expansionFactor)
{
  List<Point> expandedGalaxyLocations = new(galaxyLocations);

  foreach(int row in emptyRows)
  {
    for(int i = 0; i < galaxyLocations.Count; i++)
    {
      if(galaxyLocations[i].Row > row)
      {
        expandedGalaxyLocations[i] = 
          new Point(
            expandedGalaxyLocations[i].Row + expansionFactor - 1, 
            expandedGalaxyLocations[i].Column
          );
      }
    }
  }

  foreach(int column in emptyColumns)
  {
    for(int i = 0; i < galaxyLocations.Count; i++)
    {
      if(galaxyLocations[i].Column > column)
      {
        expandedGalaxyLocations[i] = 
          new Point(
            expandedGalaxyLocations[i].Row, 
            expandedGalaxyLocations[i].Column + expansionFactor - 1
          );
      }
    }
  }

  return expandedGalaxyLocations;
}

long SumDistancesBetweenGalaxies(List<Point> galaxyLocations)
{
  long sum = 0;

  for(int i = 0; i < galaxyLocations.Count - 1; i++)
  {
    Point galaxy = galaxyLocations[i];

    for(int j = i + 1; j < galaxyLocations.Count; j++)
    {
      Point otherGalaxy = galaxyLocations[j];

      int distance = ManhattanDistance(galaxy, otherGalaxy);

      sum += distance;
    }
  }

  return sum;
}

int ManhattanDistance(Point a, Point b)
{
  return Math.Abs(a.Row - b.Row) + Math.Abs(a.Column - b.Column);
}