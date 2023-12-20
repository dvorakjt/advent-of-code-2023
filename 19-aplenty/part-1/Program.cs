string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException
  (
    "Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/19"
  );
}

string[] input = File.ReadAllLines(inputFilePath);

InputParser inputParser = new(input);

Dictionary<string, Workflow> workflows = inputParser.ParseWorkflows();
List<Part> parts = inputParser.ParseParts();
PartSorter partSorter = new(workflows, parts);

Console.WriteLine(partSorter.AcceptedPartsRatingSum);