using System.Text.RegularExpressions;

string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file input.txt not found in current directory.");
}

string[] input = File.ReadAllLines(inputFilePath);

Func<int, int, int> countWaysToWin = (raceTime, recordDistance) =>
{
  int chargingTime = 0;
  int possibleWaysToWin = 0;

  for( ; raceTime >= 0; raceTime--, chargingTime++)
  {
    if(raceTime * chargingTime > recordDistance) possibleWaysToWin++;
  }

  return possibleWaysToWin;
};

List<int> raceTimes = new(Regex.Matches(input[0], @"\d+").Select(match => int.Parse(match.Value)));
List<int> raceRecords = new(Regex.Matches(input[1], @"\d+").Select(match => int.Parse(match.Value)));

int marginForError = 1;

for(int i = 0; i < raceTimes.Count; i++)
{
  marginForError *= countWaysToWin(raceTimes[i], raceRecords[i]);
}

Console.WriteLine($"The margin for error is {marginForError}");