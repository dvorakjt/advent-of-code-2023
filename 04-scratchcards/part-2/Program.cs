string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/4");
}

string[] cards = File.ReadAllLines(inputFilePath);

int[] copies = new int[cards.Length];

Array.Fill(copies, 1);

for(int i = 0; i < cards.Length; i++)
{
  var scratchCard = new ScratchCard(cards[i]);

  for(int j = i + 1; j <= i + scratchCard.Value && j < copies.Length; j++)
  {
    copies[j] += copies[i];
  }
}

int scratchCardCount = copies.Sum();

Console.WriteLine($"The total number of scratch cards is {scratchCardCount}");