struct PulseDetails
{
  public string OriginId { get; private set; }
  public string DestinationId { get; private set; }
  public Pulse Pulse { get; private set; }

  public PulseDetails(string originId, string destinationId, Pulse pulse)
  {
    OriginId = originId;
    DestinationId = destinationId;
    Pulse = pulse;
  }
}