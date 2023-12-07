string inputFilePath = "./input.txt";

if (!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file input.txt not found in the current directory.");
}

string[] input = File.ReadAllLines(inputFilePath);

int sum = 0;

for(int i = 0; i < input.Length; i++)
{
  string line = input[i];
  int currentNumStartIndex = -1;
  string currentNum = "";

  for(int j = 0; j < line.Length; j++)
  {
    if(line[j] - '0' >= 0 && line[j] - '0' <= 9)
    {
      if(currentNum.Length == 0)
      {
        currentNumStartIndex = j;
      }

      currentNum += line[j];
    }
    else if(currentNum.Length > 0)
    {
      if(IsAdjacentToSymbol(i, currentNumStartIndex, currentNumStartIndex + currentNum.Length - 1))
      {
        sum += int.Parse(currentNum);
      }

      currentNum = "";
    }
  }
  
  if(currentNum.Length > 0)
  {
    if(IsAdjacentToSymbol(i, currentNumStartIndex, currentNumStartIndex + currentNum.Length - 1))
    {
      sum += int.Parse(currentNum);
    }
  }
}

Console.WriteLine($"The sum of all part numbers is {sum}");

bool IsSymbol(char c) 
{
  return c != '.' && (c - '0' < 0 || c - '0' > 9);
};

bool IsAdjacentToSymbol(int line, int startIndex, int endIndex)
{
  if(line > 0)
  {
    int s = startIndex > 0 ? startIndex - 1 : startIndex;
    int e = endIndex + 1 < input[line - 1].Length ? endIndex + 1 : endIndex;

    for(int i = s; i <= e; i++) {
      char c = input[line - 1][i];
      if(IsSymbol(c))
      {
        return true;
      }
    }
  }
  if(startIndex > 0)
  {
    if(IsSymbol(input[line][startIndex - 1]))
    {
      return true;
    }
  }
  if(endIndex < input[line].Length - 1)
  {
    if(IsSymbol(input[line][endIndex + 1]))
    {
      return true;
    }
  }
  if(line < input.Length - 1)
  {
    int s = startIndex > 0 ? startIndex - 1 : startIndex;
    int e = endIndex + 1 < input[line + 1].Length ? endIndex + 1 : endIndex;

    for(int i = s; i <= e; i++) {
      char c = input[line + 1][i];
      if(IsSymbol(c))
      {
        return true;
      }
    }
  }

  return false;
};