class LensBox
{
  Dictionary<string, LinkedListNode<int>> LabeledLenses = new();
  LinkedList<int> OrderedLenses = new();

  public void AddOrUpdateLens(string label, int focalLength)
  {
    if(LabeledLenses.ContainsKey(label))
    {
      LabeledLenses[label].Value = focalLength;
    }
    else
    {
      var node = OrderedLenses.AddLast(focalLength);
      LabeledLenses[label] = node;
    }
  }

  public void RemoveLens(string label)
  {
    if(LabeledLenses.ContainsKey(label))
    {
      OrderedLenses.Remove(LabeledLenses[label]);
      LabeledLenses.Remove(label);
    }
  }

  public int CalculateFocusingPower(int boxIndex)
  {
    int boxMultiplier = boxIndex + 1;
    int focusingPower = 0;

    int lensNumber = 1;
    var lens = OrderedLenses.First;

    while(lens != null)
    {
      focusingPower += boxMultiplier * lensNumber * lens.Value;
      
      lens = lens.Next;
      lensNumber++;
    }

    return focusingPower;
  }

}