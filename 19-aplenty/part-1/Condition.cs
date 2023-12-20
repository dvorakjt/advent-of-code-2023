class Condition
{
  public string Destination { get; private set; }
  public Predicate<Part> MeetsCondition;

  public Condition(string destination, Predicate<Part> meetsCondition)
  {
    Destination = destination;
    MeetsCondition = meetsCondition;
  }
}