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

List<string> expandedGalaxyChart = GetExpandedGalaxyChart(input);
List<Point> galaxyLocations = LocateGalaxies(expandedGalaxyChart);
int galaxyDistancesSum = SumDistancesBetweenGalaxies(galaxyLocations);

Console.WriteLine($"The sum of the shortest distances between all pairs of galaxies is {galaxyDistancesSum}");


List<string> GetExpandedGalaxyChart(string[] input)
{
  List<int> rowsToExpand = new();
  List<int> columnsToExpand = new();

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

    if(isEmptyRow) rowsToExpand.Add(i);
  }

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

    if(isEmptyColumn) columnsToExpand.Add(i);
  }

  List<string> expandedGalaxyChart = new List<string>(input);

  for(int i = 0; i < columnsToExpand.Count; i++)
  {
    int offset = i + 1;
    int spliceIndex = columnsToExpand[i] + offset;

    for(int j = 0; j < expandedGalaxyChart.Count; j++)
    {
      expandedGalaxyChart[j] = expandedGalaxyChart[j].Insert(spliceIndex, ".");
    }
  }

  for(int i = 0; i < rowsToExpand.Count; i++)
  {
    int offset = i + 1;
    int spliceIndex = rowsToExpand[i] + offset;

    expandedGalaxyChart.Insert(spliceIndex, new string('.', expandedGalaxyChart[0].Length));
  }

  return expandedGalaxyChart;
}

List<Point> LocateGalaxies(List<string> galaxyChart)
{
  List<Point> galaxyLocations = new();

  for(int i = 0; i < galaxyChart.Count; i++)
  {
    for(int j = 0; j < galaxyChart[i].Length; j++)
    {
      if(galaxyChart[i][j] == '#') 
      {
        Point galaxyLocation = new Point(i, j);
        galaxyLocations.Add(galaxyLocation);
      }
    }
  }

  return galaxyLocations;
}

int SumDistancesBetweenGalaxies(List<Point> galaxyLocations)
{
  int sum = 0;

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
  return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
}