static class CardRanks 
{
  static List<char> FaceCards = new(){ 'T', 'Q', 'K', 'A'};
  static int MinRank = 2;
  static int MaxNumericCard = 9;

  static Dictionary<char, int> Ranks = new();
  
  static CardRanks()
  {
    Ranks.Add('J', MinRank - 1);

    int currentRank = MinRank;
    
    for(; currentRank <= MaxNumericCard; currentRank++)
    {
      char card = (char)(currentRank + '0');
      Ranks.Add(card, currentRank);
    }

    foreach(var faceCard in FaceCards)
    {
      Ranks.Add(faceCard, currentRank++);
    }
  }
  
  public static int GetRank(char card)
  {
    return Ranks[card];
  }
}