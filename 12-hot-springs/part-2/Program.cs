string[] input = File.ReadAllLines("input.txt");

var acs = InputParser.GetIncompleteConditionRecordsFromInput(input);

Console.WriteLine(acs.Sum(ac => ac.PossibleArrangements));