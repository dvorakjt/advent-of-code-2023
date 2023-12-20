class ConjunctionModule : AbstractModule
{
  private HashSet<string> LowPulseEmittingModuleIds;

  public ConjunctionModule(string id, List<string> destinationIds, HashSet<string> inputIds) : base(id, destinationIds)
  {
    LowPulseEmittingModuleIds = new(inputIds);
  }

  public override List<PulseDetails> ProcessPulse(Pulse input, string originId)
  {
    if(input == Pulse.HIGH)
    {
      LowPulseEmittingModuleIds.Remove(originId);
    }
    else
    {
      LowPulseEmittingModuleIds.Add(originId);
    }

    Pulse output = LowPulseEmittingModuleIds.Count == 0 ? Pulse.LOW : Pulse.HIGH;

    return CreateOutputList(output);
  }
}