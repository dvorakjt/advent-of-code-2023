static class InputParser
{
  public static ModuleNetwork ParseInput(string[] input)
  {
    TrimAllLines(input);

    Dictionary<string, HashSet<string>> conjunctionModuleInputIds = new();
    
    foreach(string moduleIdAndDestinationIds in input)
    {
      if(IsConjunctionModule(moduleIdAndDestinationIds))
      {
        string id = GetId(moduleIdAndDestinationIds);
        conjunctionModuleInputIds[id] = new();
      }
    }

    foreach(string moduleIdAndDestinationIds in input)
    {
      string originId = GetId(moduleIdAndDestinationIds);
      List<string> destinationIds = GetDestinationIds(moduleIdAndDestinationIds);
      
      foreach(string destinationId in destinationIds)
      {
        if(conjunctionModuleInputIds.ContainsKey(destinationId))
        {
          conjunctionModuleInputIds[destinationId].Add(originId);
        }
      }
    }

    Dictionary<string, AbstractModule> modules = new();

    foreach(string moduleIdAndDestinationIds in input)
    {
      string id = GetId(moduleIdAndDestinationIds);
      List<string> destinationIds = GetDestinationIds(moduleIdAndDestinationIds);

      AbstractModule module;

      if(IsBroadcaster(moduleIdAndDestinationIds))
      {
        module = new BroadcastModule(destinationIds);
      }
      else if(IsConjunctionModule(moduleIdAndDestinationIds))
      {
        module = new ConjunctionModule(id, destinationIds, conjunctionModuleInputIds[id]);
      }
      else
      {
        module = new FlipFlopModule(id, destinationIds);
      }

      modules[id] = module;
    }

    return new ModuleNetwork(modules);
  }

  private static void TrimAllLines(string[] input)
  {
    for(int i = 0; i < input.Length; i++)
    {
      input[i] = input[i].Trim();
    }
  }

  private static bool IsConjunctionModule(string s)
  {
    return s[0] == '&';
  }

  private static bool IsFlipFlopModule(string s)
  {
    return s[0] == '%';
  }

  private static bool IsBroadcaster(string s)
  {
    return s.Contains(BroadcastModule.BROADCASTER_ID);
  }

  private static string GetId(string moduleIdAndDestinationIds)
  {
    string moduleId = moduleIdAndDestinationIds.Split(' ')[0];

    if(IsConjunctionModule(moduleId) || IsFlipFlopModule(moduleId))
    {
      moduleId = moduleId[1..];
    } 

    return moduleId;
  }

  private static List<string> GetDestinationIds(string moduleIdAndDestinationIds)
  {
    return moduleIdAndDestinationIds.Split(" -> ")[1].Split(", ").ToList();
  }
}