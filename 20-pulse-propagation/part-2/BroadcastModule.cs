
class BroadcastModule : AbstractModule
{

  public const string BROADCASTER_ID = "broadcaster";

  public BroadcastModule(List<string> destinationIds) : base(BROADCASTER_ID, destinationIds) {}

  public override List<PulseDetails> ProcessPulse(Pulse input, string originId)
  {
    return CreateOutputList(input);
  }
}