using System.Text.RegularExpressions;

static class InputParser
{
  public static string GetDirections(string input)
  {
    return Regex.Match(input, "[LR]+").Value;
  }

  public static DesertMap GetMap(string input)
  {
    string[] inputLines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

    DesertMap map = new();

    for(int i = 1; i < inputLines.Length; i++)
    {
      string[] identifierAndAdjacentNodes = inputLines[i].Split(" = ");
      string identifier = identifierAndAdjacentNodes[0];
      string[] adjacentNodes = identifierAndAdjacentNodes[1].Substring(1, identifierAndAdjacentNodes[1].Length - 2).Split(", ");
      
      map.AddNode(identifier, adjacentNodes[0], adjacentNodes[1]);
    }

    return map;
  }
}