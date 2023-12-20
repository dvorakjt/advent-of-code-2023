string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException
  (
    "Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/19"
  );
}

string[] input = File.ReadAllLines(inputFilePath);

ModuleNetwork moduleNetwork = InputParser.ParseInput(input);

long requiredButtonPresses = moduleNetwork.CountButtonPressesRequiredToSendPulseToModule("output", Pulse.HIGH);

Console.WriteLine(requiredButtonPresses);
