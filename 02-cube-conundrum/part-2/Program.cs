﻿using System;
using System.IO;
using System.Text.RegularExpressions;

string inputFilePath = "./input.txt";

if (!File.Exists(inputFilePath))
{
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/2");
}

string[] games = File.ReadAllLines(inputFilePath);

string colorWithCountPattern = "(\\d+)\\s(red|green|blue)";
Regex colorWithCountRegex = new Regex(colorWithCountPattern);

int cubeSetPowerSum = 0;

foreach(string game in games)
{
  Dictionary<string, int> minimumColorCounts = new Dictionary<string, int>()
  {
    { "red", 0 },
    { "green", 0 },
    { "blue", 0 }
  };

  var colorCounts = colorWithCountRegex.Matches(game);

  if(colorCounts == null) continue;

  foreach (Match colorCount in colorCounts)
  {
    var (Count, Color) = GetCountAndColor(colorCount);
    if(Count > minimumColorCounts[Color])
    {
      minimumColorCounts[Color] = Count;
    }
  }

  int cubeSetPower = minimumColorCounts["red"] * minimumColorCounts["green"] * minimumColorCounts["blue"];

  cubeSetPowerSum += cubeSetPower;
}

Console.WriteLine($"The sum of all minimum cube set powers is: {cubeSetPowerSum}");

(int Count, string Color) GetCountAndColor(Match m) 
{
  int Count = int.Parse(m.Groups[1].Captures[0].Value);
  string Color = m.Groups[2].Captures[0].Value;

  return (Count, Color);
};