class ModuleNetwork
{
  private LinkedList<PulseDetails> PulseQueue = new();

  private Dictionary<string, AbstractModule> Modules;

  public int HighPulsesFired { get; private set; } = 0;
  public int LowPulsesFired { get; private set; } = 0;

  public ModuleNetwork(Dictionary<string, AbstractModule> modules)
  {
    Modules = new(modules);
  }
  
  public void FirePulseNTimes(int n)
  {
    for(int i = 0; i < n; i++)
    {
      FirePulse();
    }
  }

  private void FirePulse()
  {
    PulseQueue.AddLast(new PulseDetails("button", BroadcastModule.BROADCASTER_ID, Pulse.LOW));

    while(PulseQueue.First != null)
    {
      PulseDetails pulseDetails = PulseQueue.First.Value;
      PulseQueue.RemoveFirst();

      if(pulseDetails.Pulse == Pulse.HIGH)
      {
        HighPulsesFired++;
      }
      else
      {
        LowPulsesFired++;
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
  }
}