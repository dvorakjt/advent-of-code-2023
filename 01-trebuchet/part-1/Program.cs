string inputFilePath = "./input.txt";

if(!File.Exists(inputFilePath)) {
  throw new FileNotFoundException("Input file 'input.txt' not found in current directory. Please retrieve your input file from https://adventofcode.com/2023/day/1");
}

string[] input = File.ReadAllLines(inputFilePath);

int sum = input.Sum(s => {
  char firstDigit = s.FirstOrDefault(IsDigit, '0');
  char lastDigit = s.LastOrDefault(IsDigit, '0');

  int firstInt = ToInt(firstDigit) * 10;
  int lastInt = ToInt(lastDigit);

  return firstInt + lastInt;
});

Console.WriteLine($"The sum of all of the calibration values in the document is {sum}");

bool IsDigit(char c) 
{
  int asInt = ToInt(c);
  return asInt >= 0 && asInt <= 9;
};

int ToInt(char c)
{
  return c - '0';
}