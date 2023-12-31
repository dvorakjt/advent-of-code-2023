﻿string inputFilePath = "input.txt";

if(!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/8");
}

string input = File.ReadAllText(inputFilePath);

DesertMap map = InputParser.GetMap(input);
string directions = InputParser.GetDirections(input);

Console.WriteLine("Read input. Navigating map...");

long steps = await map.Navigate(directions);

Console.WriteLine($"Map navigated. The total number of steps was {steps}");
