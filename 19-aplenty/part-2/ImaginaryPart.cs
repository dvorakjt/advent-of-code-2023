global using MinMax = (int Min, int Max);

class ImaginaryPart
{
  public readonly MinMax X;
  public readonly MinMax M;
  public readonly MinMax A;
  public readonly MinMax S;

  public ImaginaryPart()
  {
    X = (1, 4000);
    M = (1, 4000);
    A = (1, 4000);
    S = (1, 4000);
  }

  public ImaginaryPart
  (
    MinMax x,
    MinMax m,
    MinMax a,
    MinMax s
  )
  {
    X = x;
    M = m;
    A = a;
    S = s;
  }

  public ImaginaryPart
  (
    ImaginaryPart original,
    PartProperty propertyToOverwrite,
    MinMax partPropertyValue
  )
  {
    if(propertyToOverwrite == PartProperty.X)
    {
      X = partPropertyValue;
    }
    else
    {
      X = original.X;
    }

    if(propertyToOverwrite == PartProperty.M)
    {
      M = partPropertyValue;
    }
    else
    {
      M = original.M;
    }

    if(propertyToOverwrite == PartProperty.A)
    {
      A = partPropertyValue;
    }
    else
    {
      A = original.A;
    }

    if(propertyToOverwrite == PartProperty.S)
    {
      S = partPropertyValue;
    }
    else
    {
      S = original.S;
    }
  }

  public (ImaginaryPart? BelowMax, ImaginaryPart? AtOrAboveMax) SplitAtMax(PartProperty partProperty, int max)
  {
    ImaginaryPart? belowMax = null;
    ImaginaryPart? atOrAboveMax = null;

    (int Min, int Max) partPropertyValue;

    switch(partProperty)
    {
      case PartProperty.X:
        partPropertyValue = X;
        break;
      case PartProperty.M:
        partPropertyValue = M;
        break;
      case PartProperty.A:
        partPropertyValue = A;
        break;
      case PartProperty.S:
        partPropertyValue = S;
        break;
      default:
        throw new InvalidOperationException("PartProperty must be one of X, M, A or X");
    }

    if(partPropertyValue.Min < max)
    {
      belowMax = new ImaginaryPart
      (
        this,
        partProperty,
        (partPropertyValue.Min, Math.Min(partPropertyValue.Max, max - 1))
      );
    }

    if(partPropertyValue.Max >= max)
    {
      atOrAboveMax = new ImaginaryPart
      (
        this,
        partProperty,
        (Math.Max(partPropertyValue.Min, max), partPropertyValue.Max)
      );
    }

    return (belowMax, atOrAboveMax);
  }

    public (ImaginaryPart? AboveMin, ImaginaryPart? AtOrBelowMin) SplitAtMin(PartProperty partProperty, int min)
  {
    ImaginaryPart? aboveMin = null;
    ImaginaryPart? atOrBelowMin = null;

    (int Min, int Max) partPropertyValue;

    switch(partProperty)
    {
      case PartProperty.X:
        partPropertyValue = X;
        break;
      case PartProperty.M:
        partPropertyValue = M;
        break;
      case PartProperty.A:
        partPropertyValue = A;
        break;
      case PartProperty.S:
        partPropertyValue = S;
        break;
      default:
        throw new InvalidOperationException("PartProperty must be one of X, M, A or X");
    }

    if(partPropertyValue.Max > min)
    {
      aboveMin = new ImaginaryPart
      (
        this,
        partProperty,
        (Math.Max(partPropertyValue.Min, min + 1), partPropertyValue.Max)
      );
    }

    if(partPropertyValue.Min <= min)
    {
      atOrBelowMin = new ImaginaryPart
      (
        this,
        partProperty,
        (partPropertyValue.Min, Math.Min(partPropertyValue.Max, min))
      );
    }

    return (aboveMin, atOrBelowMin);
  }

  public long CountPossibleCombinations()
  {
    return (long)(X.Max + 1 - X.Min) * (M.Max + 1 - M.Min) * (A.Max + 1 - A.Min) * (S.Max + 1 - S.Min);
  }
}