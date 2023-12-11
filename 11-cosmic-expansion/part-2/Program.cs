string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/11");
}

string[] starChart = File.ReadAllLines(inputFilePath);

for(int i = 0; i < starChart.Length; i++)
{
  starChart[i] = starChart[i].Trim();
}

List<int> rowsToExpand = new();
List<int> columnsToExpand = new();

for(int i = 0; i < starChart.Length; i++)
{
  bool isEmptyRow = true;
  
  for(int j = 0; j < starChart[i].Length; j++)
  {
    if(starChart[i][j] == '#') {
      isEmptyRow = false;
      break;
    }
  }

  if(isEmptyRow) rowsToExpand.Add(i);
}

for(int i = 0; i < starChart[0].Length; i++)
{
  bool isEmptyColumn = true;
  
  for(int j = 0; j < starChart.Length; j++)
  {
    if(starChart[j][i] == '#'){
      isEmptyColumn = false;
      break;
    } 
  }

  if(isEmptyColumn) columnsToExpand.Add(i);
}


List<Point> galaxies = new();

for(int i = 0; i < starChart.Length; i++)
{
  for(int j = 0; j < starChart[i].Length; j++)
  {
    if(starChart[i][j] == '#') 
    {
      Point galaxyLocation = new Point(i, j);
      galaxies.Add(galaxyLocation);
    }
  }
}

List<Point> modifiedGalaxies = new(galaxies);

long galaxiesDistanceSum = 0;

foreach(var rowToExpand in rowsToExpand)
{
  for(int i = 0; i < galaxies.Count; i++)
  {
    if(galaxies[i].Row > rowToExpand)
    {
      modifiedGalaxies[i] = new Point(modifiedGalaxies[i].Row + 1000000 - 1, modifiedGalaxies[i].Column);
    }
  }
}

foreach(var colToExpand in columnsToExpand)
{
  for(int i = 0; i < galaxies.Count; i++)
  {
    if(galaxies[i].Column > colToExpand)
    {
      modifiedGalaxies[i] = new Point(modifiedGalaxies[i].Row, modifiedGalaxies[i].Column + 1000000 - 1);
    }
  }
}

for(int i = 0; i < modifiedGalaxies.Count - 1; i++)
{
  Point galaxy = modifiedGalaxies[i];

  for(int j = i + 1; j < modifiedGalaxies.Count; j++)
  {
    Point otherGalaxy = modifiedGalaxies[j];

    int distance = ManhattanDistance(galaxy, otherGalaxy);

    galaxiesDistanceSum += distance;
  }
}

int ManhattanDistance(Point a, Point b)
{
  return Math.Abs(a.Row - b.Row) + Math.Abs(a.Column - b.Column);
}

Console.WriteLine(galaxiesDistanceSum);