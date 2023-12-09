string inputFilePath = "./input.txt";

if (!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/4");
}

string[] cards = File.ReadAllLines(inputFilePath);

int totalPoints = cards.Sum(card => new ScratchCard(card).Value);

Console.WriteLine($"The total points won on all of the scratch cards is {totalPoints}");