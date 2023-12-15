using System.Text.RegularExpressions;

string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException
  (
    "Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/15"
  );
}

string input = File.ReadAllText(inputFilePath);

string[] sequence = input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

List<LensBox> lensBoxes = new();

for(int i = 0; i < 256; i++)
{
  lensBoxes.Add(new LensBox());
}

foreach(string step in sequence)
{
  string label = ExtractLabel(step);
  int boxId = HashString(label);
  LensBox lensBox = lensBoxes[boxId];

  Operation operation = GetOperation(step);

  if(operation == Operation.ADD)
  {
    int focalLength = GetFocalLength(step);
    lensBox.AddOrUpdateLens(label, focalLength);
  }
  else
  {
    lensBox.RemoveLens(label);
  }
}

int focusingPower = lensBoxes.Select((lensBox, i) => lensBox.CalculateFocusingPower(i)).Sum();

Console.WriteLine($"The focusing power of the lens configuration is {focusingPower}");


string ExtractLabel(string step)
{
  return Regex.Match(step, "[a-z]+", RegexOptions.IgnoreCase).Value;
}

int HashString(string s)
{
  int result = 0;

  for(int i = 0; i < s.Length; i++)
  {
    result = HashCharacter(s[i], result);
  }

  return result;
}

int HashCharacter(char c, int previousValue)
{
  return (previousValue + c) * 17 % 256;
}

Operation GetOperation(string step)
{
  if(step.Contains('=')) return Operation.ADD;

  return Operation.REMOVE;
}

int GetFocalLength(string step)
{
  return int.Parse(step.Split('=')[1]);
}