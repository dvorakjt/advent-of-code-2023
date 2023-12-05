string inputFilePath = "./input.txt";

if (!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file input.txt not found in the current directory.");
}

string[] input = File.ReadAllLines(inputFilePath);

Predicate<char> isSymbol = c => {
  return c != '.' && (c - '0' < 0 || c - '0' > 9);
};

Predicate<(int Line, int StartIndex, int EndIndex)> isAdjacentToSymbol = location => {
  var (Line, StartIndex, EndIndex) = location;

  if(Line > 0)
  {
    int s = StartIndex > 0 ? StartIndex - 1 : StartIndex;
    int e = EndIndex + 1 < input[Line - 1].Length ? EndIndex + 1 : EndIndex;

    for(int i = s; i <= e; i++) {
      char c = input[Line - 1][i];
      if(isSymbol(c))
      {
        return true;
      }
    }
  }
  if(StartIndex > 0)
  {
    if(isSymbol(input[Line][StartIndex - 1]))
    {
      return true;
    }
  }
  if(EndIndex < input[Line].Length - 1)
  {
    if(isSymbol(input[Line][EndIndex + 1]))
    {
      return true;
    }
  }
  if(Line < input.Length - 1)
  {
    int s = StartIndex > 0 ? StartIndex - 1 : StartIndex;
    int e = EndIndex + 1 < input[Line + 1].Length ? EndIndex + 1 : EndIndex;

    for(int i = s; i <= e; i++) {
      char c = input[Line + 1][i];
      if(isSymbol(c))
      {
        return true;
      }
    }
  }

  return false;
};

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
      if(isAdjacentToSymbol((i, currentNumStartIndex, currentNumStartIndex + currentNum.Length - 1)))
      {
        sum += int.Parse(currentNum);
      }

      currentNum = "";
    }
  }
  
  if(currentNum.Length > 0)
  {
    if(isAdjacentToSymbol((i, currentNumStartIndex, currentNumStartIndex + currentNum.Length - 1)))
    {
      sum += int.Parse(currentNum);
    }
  }
}

Console.WriteLine($"The sum of all part numbers is {sum}");