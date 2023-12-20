class FlipFlopModule : AbstractModule
{
  private bool On = false;

  public FlipFlopModule(string id, List<string> destinationIds) : base(id, destinationIds) {}
  public override List<PulseDetails> ProcessPulse(Pulse input, string originKey)
  {
    if(input == Pulse.HIGH) return new();

    Pulse output;

    if(On)
    {
      On = false;
      output = Pulse.LOW;
    }
    else
    {
      On = true;
      output = Pulse.HIGH;
    }

    return CreateOutputList(output);
  }
}