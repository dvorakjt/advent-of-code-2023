class BrickPile
{
  private List<Brick> Bricks;
  private int[,,] CubeGrid;
  private Dictionary<int, HashSet<int>> SupportedBy = new();
  public int DisintegratableBricks { get; private set; }

  public BrickPile(List<Brick> bricks)
  {
    Bricks = new(bricks);

    int cubeWidth = bricks.Max(brick => brick.X2) - bricks.Min(brick => brick.X1) + 1;
    int cubeDepth = bricks.Max(brick => brick.Y2) - bricks.Min(brick => brick.Y1) + 1;
    int cubeHeight = bricks.Sum(brick => brick.Height); 

    CubeGrid = new int[cubeWidth, cubeDepth, cubeHeight];

    PileBricks();
    DisintegratableBricks = CountDisintegratableBricks();
  }

  private void PileBricks()
  {
    for(int i = 0; i < Bricks.Count; i++)
    {
      Brick brick = Bricks[i];
      int lowestLevel = 0;

      for(int z = brick.Z1; z >= 0; z--)
      {
        for(int x = brick.X1; x <= brick.X2; x++)
        { 
          for(int y = brick.Y1; y <= brick.Y2; y++)
          {
            if(CubeGrid[x,y,z] > 0)
            {
              lowestLevel = z + 1;
              if(!SupportedBy.ContainsKey(brick.Id))
              {
                SupportedBy.Add(brick.Id, []);
              }
              SupportedBy[brick.Id].Add(CubeGrid[x,y,z]);
            }
          }
        }
        if(lowestLevel > 0) break;
      }

      for(int z = lowestLevel; z < lowestLevel + brick.Height; z++)
      {
        for(int x = brick.X1; x <= brick.X2; x++)
        { 
          for(int y = brick.Y1; y <= brick.Y2; y++)
          {
            CubeGrid[x,y,z] = brick.Id;
          }
        }
      }
    }
  }

  private int CountDisintegratableBricks()
  {
    int disintegratableBrickCount = Bricks.Count;

    foreach(var brick in Bricks)
    {
      foreach(var supportingBricks in SupportedBy.Values)
      {
        if(supportingBricks.Count == 1 && supportingBricks.Contains(brick.Id))
        {
          disintegratableBrickCount--;
          break;
        }
      }
    }
    
    return disintegratableBrickCount;
  }
}