class BrickPile
{
  private readonly List<Brick> Bricks;
  private readonly int[,,] CubeGrid;
  private readonly Dictionary<int, HashSet<int>> SupportedBy = [];
  private readonly Dictionary<int, HashSet<int>> Supports = [];

  public int ChainReactionDisintegratedBricksSum { get; private set; }

  public BrickPile(List<Brick> bricks)
  {
    Bricks = new(bricks);

    int cubeWidth = bricks.Max(brick => brick.X2) + 1;
    int cubeDepth = bricks.Max(brick => brick.Y2) + 1;
    int cubeHeight = bricks.Max(brick => brick.Z2) + 1;

    CubeGrid = new int[cubeWidth, cubeDepth, cubeHeight];

    PileBricks();
    ChainReactionDisintegratedBricksSum = SumChainReactionDisintegratedBricks();
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
            //If CubeGrid[x,y,z] > 0, a brick id was stored there
            if(CubeGrid[x,y,z] > 0)
            {
              lowestLevel = z + 1;
              int supportingBrickId = CubeGrid[x,y,z];

              if(!SupportedBy.ContainsKey(brick.Id))
              {
                SupportedBy.Add(brick.Id, []);
              }
              if(!Supports.ContainsKey(supportingBrickId))
              {
                Supports.Add(supportingBrickId, []);
              }

              SupportedBy[brick.Id].Add(supportingBrickId);
              Supports[supportingBrickId].Add(brick.Id);
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

  private int SumChainReactionDisintegratedBricks()
  {
    int sum = 0;

    foreach(var brick in Bricks)
    {
      sum += CountOtherBricksDisintegratedInChainReaction(brick.Id);
    }

    return sum;
  }

  private int CountOtherBricksDisintegratedInChainReaction(int brickId)
  {
    HashSet<int> disintegratedBrickIds = [brickId];
    LinkedList<int> disintegrationQueue = new();
    disintegrationQueue.AddLast(brickId);
    var disintegrationQueueNode = disintegrationQueue.First;
    
    while(disintegrationQueueNode != null)
    {
      int disintegratedBrickId = disintegrationQueueNode.Value;

      if(Supports.TryGetValue(disintegratedBrickId, out HashSet<int>? supportedBricks))
      {
        foreach(var supportedBrickId in supportedBricks)
        {
          HashSet<int> remainingSupports = 
            SupportedBy[supportedBrickId]
              .Where(id => !disintegratedBrickIds.Contains(id))
              .ToHashSet();
        
          if(remainingSupports.Count == 0)
          {
            disintegratedBrickIds.Add(supportedBrickId);
            disintegrationQueue.AddLast(supportedBrickId);
          }
        }
      }
      
      disintegrationQueueNode = disintegrationQueueNode.Next;
    }

    //count of other disintegrated bricks is count - 1 because brickId is included in the set of disintegrated bricks
    return disintegratedBrickIds.Count - 1;
  }
}