using System;
using System.IO;
using System.Linq;

string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath)) {
  throw new FileNotFoundException("Input file input.txt not found");
}

string[] input = File.ReadAllLines(inputFilePath);

Func<char, int> toInt = c => c - '0';

Func<char, bool> isDigit = c => {
  int asInt = toInt(c);
  return asInt >= 0 && asInt <= 9;
};

int sum = input.Sum(s => {
  char firstDigit = s.FirstOrDefault(isDigit, '0');
  char lastDigit = s.LastOrDefault(isDigit, '0');

  int firstInt = toInt(firstDigit) * 10;
  int lastInt = toInt(lastDigit);

  return firstInt + lastInt;
});

Console.WriteLine($"The sum of all of the calibration values in the document is {sum}");
