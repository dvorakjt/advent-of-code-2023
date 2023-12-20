abstract class AbstractModule {
  
  protected string Id;
  protected List<string> DestinationIds;

  public AbstractModule(string id, List<string> destinationIds)
  {
    Id = id;
    DestinationIds = new(destinationIds);
  }
  
  abstract public List<PulseDetails> ProcessPulse(Pulse input, string originId);

  protected List<PulseDetails> CreateOutputList(Pulse output)
  {
    return DestinationIds.Select(destinationId => new PulseDetails(Id, destinationId, output)).ToList();
  }
}