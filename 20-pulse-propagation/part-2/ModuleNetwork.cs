class ModuleNetwork
{
  private LinkedList<PulseDetails> PulseQueue = new();

  private Dictionary<string, AbstractModule> Modules;

  public ModuleNetwork(Dictionary<string, AbstractModule> modules)
  {
    Modules = new(modules);
  }
  
  public long CountButtonPressesRequiredToSendPulseToModule(string moduleId, Pulse pulse)
  {
    long presses = 0;
    bool sentPulseToModule;
    do
    {
      presses++;
      sentPulseToModule = TrySendPulseToModule(moduleId, pulse);

      Console.SetCursorPosition(0, Console.CursorTop);
      Console.Write(presses);

    } while(!sentPulseToModule);

    return presses;
  }

  private bool TrySendPulseToModule(string moduleId, Pulse pulse)
  {
    PulseQueue.AddLast(new PulseDetails("button", BroadcastModule.BROADCASTER_ID, Pulse.LOW));

    while(PulseQueue.First != null)
    {
      PulseDetails pulseDetails = PulseQueue.First.Value;
      PulseQueue.RemoveFirst();

      if(pulseDetails.DestinationId == moduleId && pulseDetails.Pulse == pulse)
      {
        return true;
      }

      if(Modules.ContainsKey(pulseDetails.DestinationId))
      {
        AbstractModule destination = Modules[pulseDetails.DestinationId];
        List<PulseDetails> pulseDetailsToProcess = destination.ProcessPulse(pulseDetails.Pulse, pulseDetails.OriginId);
        
        foreach(PulseDetails unprocessedPulseDetails in pulseDetailsToProcess)
        {
          PulseQueue.AddLast(unprocessedPulseDetails);
        }
      }
    }

    return false;
  }
}