class LabeledMatrixPoint
{
  public Label Label { get; private set; }
  public int? PathIndex { get; private set; } = null;

  public LabeledMatrixPoint(Label label)
  {
    Label = label;
  }

  public LabeledMatrixPoint(Label label, int pathIndex)
  {
    Label = label;
    PathIndex = pathIndex;
  }
}