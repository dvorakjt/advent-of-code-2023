string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/7");
}

string[] input = File.ReadAllLines(inputFilePath);

List<Hand> hands = new List<Hand>();

foreach(var round in input)
{
  Hand h = new(round);
  hands.Add(h);
}

hands.Sort();

int totalWinnings = 0;

for(int i = 0; i < hands.Count; i++)
{
  Hand hand = hands[i];
  int overallRank = i + 1;
  
  totalWinnings += hand.GetWinnings(overallRank);
}

Console.WriteLine($"The total winnings from all hands is: {totalWinnings}");