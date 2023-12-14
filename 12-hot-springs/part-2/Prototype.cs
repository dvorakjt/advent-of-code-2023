// using System;
// using System.Collections.Generic;
// using System.Linq;

// class Prototype
// {
//     public static void Main(string[] args)
//   {
//     int patternLength = 100;
//     List<int> groups = new() { 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3 };

//     //add one to the right of each group  
//     List<int> transformedGroups =
//       groups
//         .Select((g, i) => i < groups.Count - 1 ? g + 1 : g)
//         .ToList();

//     int furthestLeftPosition = 0;
//     int remainingRequiredLength = transformedGroups.Sum();

//     List<List<Range>> rangesPerGroup = new();

//     foreach (var group in transformedGroups)
//     {
//         int furthestRightPosition = patternLength - remainingRequiredLength;

//         List<Range> ranges = new();

//         for (int i = furthestLeftPosition; i <= furthestRightPosition; i++)
//         {
//             ranges.Add(new Range(i, group));
//         }

//         rangesPerGroup.Add(ranges);

//         furthestLeftPosition += group;
//         remainingRequiredLength -= group;
//     }
    
//     for(int i =  1; i < rangesPerGroup.Count; i++)
//     {
//       var ranges = rangesPerGroup[i];
//       var previousRanges = rangesPerGroup[i - 1];

//       for(int j = 0; j < ranges.Count; j++)
//       {
//         int count = 0;

//         foreach(Range previousRange in previousRanges)
//         {
//           if(previousRange.Start + previousRange.Length <= ranges[j].Start)
//           {
//             count += previousRange.Possibilities; 
//           }
//         }

//         ranges[j].Possibilities = count;
//       }
//     }

//     foreach(var range in rangesPerGroup[rangesPerGroup.Count - 1])
//     {
//       Console.WriteLine(range.Possibilities);
//     }
//   }
// }