class ScratchCard {
  public int Value { get; private set; } = 0;

  public ScratchCard(string card)
  {
    DetermineValue(card);
  }

  private void DetermineValue(string card)
  {
    string[] revealedNumbers = card.Split(':')[1].Split('|');

    HashSet<string> winningNumbers = new(revealedNumbers[0].Split(' ').Where(num => !string.IsNullOrWhiteSpace(num)));

    HashSet<string> yourNumbers = new(revealedNumbers[1].Split(' ').Where(num => !string.IsNullOrWhiteSpace(num)));

    winningNumbers.IntersectWith(yourNumbers);

    Value = winningNumbers.Count;
  }
}