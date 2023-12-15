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

int sequenceSum = sequence.Select(HashString).Sum();

Console.WriteLine($"The sum of all steps in the sequence is {sequenceSum}");

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