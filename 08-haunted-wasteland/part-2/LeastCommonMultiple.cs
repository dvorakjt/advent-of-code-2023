static class LeastCommonMultiple
{
  public static long LCM(IEnumerable<long> values)
  {
    long lcm = 1;

    foreach(long value in values)
    {
      lcm = lcm * value / GCD(lcm, value);
    }

    return lcm;
  }

  private static long GCD(long a, long b)
  {
    if(b == 0) return a;

    long r = a % b;

    return GCD(b, r);
  }
}