using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath)) {
  throw new FileNotFoundException("Input file input.txt not found");
}

string[] input = File.ReadAllLines(inputFilePath);

string pattern = "(?=(\\d|one|two|three|four|five|six|seven|eight|nine))";

Regex regex = new Regex(pattern);

int sum = input.Sum(s => {
  var matches = regex.Matches(s);

  if(matches == null) {
    return 0;
  }

  string firstDigit = GetMatchedString(matches[0]);
  string lastDigit = GetMatchedString(matches[^1]);

  int firstInt = ToInt(firstDigit) * 10;
  int lastInt = ToInt(lastDigit);

  int calibrationValue = firstInt + lastInt;

  return calibrationValue;
});

Console.WriteLine($"The sum of all of the calibration values in the document is {sum}");

int ToInt(string s) {
  if(s.Length == 1) 
  {
    return s[0] - '0';
  }

  return s switch
  {
    "one" => 1,
    "two" => 2,
    "three" => 3,
    "four" => 4,
    "five" => 5,
    "six" => 6,
    "seven" => 7,
    "eight" => 8,
    "nine" => 9,
    _ => 0,
  };
};

string GetMatchedString(Match m) {
  return m.Groups[1].Captures[0].Value;
};