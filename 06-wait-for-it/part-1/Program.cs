using System.Text.RegularExpressions;

string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/6");
}

string[] input = File.ReadAllLines(inputFilePath);

List<int> raceTimes = Regex.Matches(input[0], @"\d+").Select(match => int.Parse(match.Value)).ToList();
List<int> raceRecords = Regex.Matches(input[1], @"\d+").Select(match => int.Parse(match.Value)).ToList();

int marginForError = 1;

for(int i = 0; i < raceTimes.Count; i++)
{
  marginForError *= CountWaysToWin(raceTimes[i], raceRecords[i]);
}

Console.WriteLine($"The margin for error is {marginForError}");

int CountWaysToWin(int raceTime, int recordDistance)
{
  int chargingTime = 0;
  int possibleWaysToWin = 0;

  for( ; raceTime >= 0; raceTime--, chargingTime++)
  {
    if(raceTime * chargingTime > recordDistance) possibleWaysToWin++;
  }

  return possibleWaysToWin;
};