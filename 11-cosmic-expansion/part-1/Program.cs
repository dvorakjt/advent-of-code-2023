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

Console.WriteLine(rowsToExpand.Count);
Console.WriteLine(columnsToExpand.Count);

List<string> starChart = new List<string>(input);

for(int i = 0; i < columnsToExpand.Count; i++)
{
  int offset = i + 1;
  int spliceIndex = columnsToExpand[i] + offset;

  for(int j = 0; j < starChart.Count; j++)
  {
    starChart[j] = starChart[j].Insert(spliceIndex, ".");
  }
}

for(int i = 0; i < rowsToExpand.Count; i++)
{
  int offset = i + 1;
  int spliceIndex = rowsToExpand[i] + offset;

  starChart.Insert(spliceIndex, new string('.', starChart[0].Length));
}

List<Point> galaxies = new();

for(int i = 0; i < starChart.Count; i++)
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

int galaxiesDistanceSum = 0;

for(int i = 0; i < galaxies.Count - 1; i++)
{
  Point galaxy = galaxies[i];

  for(int j = i + 1; j < galaxies.Count; j++)
  {
    Point otherGalaxy = galaxies[j];

    int distance = ManhattanDistance(galaxy, otherGalaxy);

    galaxiesDistanceSum += distance;
  }
}

int ManhattanDistance(Point a, Point b)
{
  return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
}

Console.WriteLine(galaxiesDistanceSum);