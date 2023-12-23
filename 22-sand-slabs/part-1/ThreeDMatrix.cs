class ThreeDMatrix
{
  public int[,,] Matrix;

  public ThreeDMatrix(int width, int depth, int height)
  {
    Matrix = new int[width, depth, height];
  }

  public void Print()
  {
    for(int z = 0; z < Matrix.GetLength(2); z++)
    {
      List<string> lines = new();
      bool foundId = false;

      for(int y = 0; y < Matrix.GetLength(1); y++)
      {
        string line = "";

        for(int x = 0; x < Matrix.GetLength(0); x++)
        {
          if(Matrix[x,y,z] > 0)
          {
            foundId = true;
            line += "B";
          }
          else line += "_";
        }

        lines.Add(line);
      }
      
      if(!foundId) return;
      
      Console.WriteLine(string.Join('\n', lines));
      Console.WriteLine();
    }
  }
}