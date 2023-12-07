using System;
using System.IO;
using System.Text.RegularExpressions;

string inputFilePath = "./input.txt";

if (!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file input.txt not found in the current directory.");
}

string[] games = File.ReadAllLines(inputFilePath);

string gameNumberPattern = "(?<=Game\\s)\\d+";
Regex gameNumberRegex = new Regex(gameNumberPattern);

string colorWithCountPattern = "(\\d+)\\s(red|green|blue)";
Regex colorWithCountRegex = new Regex(colorWithCountPattern);

Dictionary<string, int> maximumColorCounts = new Dictionary<string, int>()
{
  { "red", 12 },
  { "green", 13 },
  { "blue", 14 }
};

int possibleGameIDsSum = 0;

foreach(string game in games)
{
  var colorCounts = colorWithCountRegex.Matches(game);

  if(colorCounts == null) continue;

  bool possible = true;

  foreach (Match colorCount in colorCounts)
  {
    var (Count, Color) = GetCountAndColor(colorCount);
    if(Count > maximumColorCounts[Color])
    {
      possible = false;
      break;
    }
  }

  if(!possible) continue;

  Match gameNumberMatch = gameNumberRegex.Match(game);
  int gameNumber = int.Parse(gameNumberMatch.Value);

  possibleGameIDsSum += gameNumber;
}

Console.WriteLine($"The sum of the IDs of all possible games is: {possibleGameIDsSum}");

(int Count, string Color) GetCountAndColor(Match m) 
{
  int Count = int.Parse(m.Groups[1].Captures[0].Value);
  string Color = m.Groups[2].Captures[0].Value;

  return (Count, Color);
};