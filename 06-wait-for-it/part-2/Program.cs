using System.Text.RegularExpressions;

string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file input.txt not found in current directory.");
}

string[] input = File.ReadAllLines(inputFilePath);

Func<string, long> getTime = s =>
{
  return long.Parse(string.Join("", Regex.Matches(s, @"\d+").Select(match => match.Value)));
};

Func<long, long, long> countWaysToWin = (raceTime, recordDistance) =>
{
  long chargingTime = 0;
  long possibleWaysToWin = 0;

  for( ; raceTime >= 0; raceTime--, chargingTime++)
  {
    if(raceTime * chargingTime > recordDistance) possibleWaysToWin++;
  }

  return possibleWaysToWin;
};

long raceTime = getTime(input[0]);
long raceRecord = getTime(input[1]);

long waysToWin = countWaysToWin(raceTime, raceRecord);

Console.WriteLine($"The number of ways to win is {waysToWin}");