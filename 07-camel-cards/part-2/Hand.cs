struct Hand : IComparable<Hand>
{
  private const int Size = 5;
  private string Cards;
  private HandType Type;
  private int Bid;

  public Hand(string round)
  {
    Cards = round.Substring(0, Size);
    Bid = int.Parse(round.Substring(Size + 1).Trim());
    Type = GetHandType();
  }

  private HandType GetHandType()
  {
    Dictionary<char, int> cardCounts = new Dictionary<char, int>();
    int jokers = 0;

    for(int i = 0; i < Size; i++)
    {
      char card = Cards[i];

      if(card == 'J')
      {
        jokers++;
        continue;
      }

      if(!cardCounts.ContainsKey(card))
      {
        cardCounts[card] = 0;
      }
      cardCounts[card]++;
    }

    if(jokers == 5) return HandType.FIVE_OF_A_KIND;

    List<int> sortedCounts = new(cardCounts.Values.OrderByDescending(c => c));

    //add jokers to highest count
    sortedCounts[0] += jokers;

    if(sortedCounts[0] == 5) return HandType.FIVE_OF_A_KIND;
    if(sortedCounts[0] == 4) return HandType.FOUR_OF_A_KIND;
    if(sortedCounts[0] == 3)
    {
      if(sortedCounts[1] == 2) return HandType.FULL_HOUSE;
      return HandType.THREE_OF_A_KIND; 
    }
    if(sortedCounts[0] == 2)
    {
      if(sortedCounts[1] == 2) return HandType.TWO_PAIR;
      return HandType.ONE_PAIR;
    }

    return HandType.HIGH_CARD;
  }

  public int CompareTo(Hand other)
  {
    if(Type < other.Type)
    {
      return -1;
    }
    else if(Type > other.Type)
    {
      return 1;
    }
    else
    {
      for(int i = 0; i < Size; i++)
      {
        int rank = CardRanks.GetRank(Cards[i]);
        int otherRank = CardRanks.GetRank(other.Cards[i]);

        if(rank < otherRank)
        { 
          return -1;
        }
        else if(rank > otherRank)
        {
          return 1;
        }
      }

      return 0;
    }
  }

  public int GetWinnings(int overallRank)
  {
    return overallRank * Bid;
  }
}