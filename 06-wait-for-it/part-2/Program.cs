using System.Text.RegularExpressions;

string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/6");
}

string[] input = File.ReadAllLines(inputFilePath);

long raceTime = GetTime(input[0]);
long raceRecord = GetTime(input[1]);

long waysToWin = CountWaysToWin(raceTime, raceRecord);

Console.WriteLine($"The number of ways to win is {waysToWin}");

long GetTime(string s)
{
  return long.Parse(string.Join("", Regex.Matches(s, @"\d+").Select(match => match.Value)));
};

long CountWaysToWin(long raceTime, long recordDistance)
{
  long chargingTime = 0;
  long possibleWaysToWin = 0;

  for( ; raceTime >= 0; raceTime--, chargingTime++)
  {
    if(raceTime * chargingTime > recordDistance) possibleWaysToWin++;
  }

  return possibleWaysToWin;
};