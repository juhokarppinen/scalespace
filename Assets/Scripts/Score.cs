public class Score
{
  public int Hits { get { return hits; } }
  public int Misses { get { return misses; } }
  public int Total { get { return hits + misses; } }
  public int Accuracy { get { return Total == 0 ? 0 : (int)(100 * ((1f * Hits) / (1f * Total))); } }

  private int hits;
  private int misses;

  public void Hit()
  {
    hits += 1;
  }

  public void Miss()
  {
    misses += 1;
  }

  public void Reset()
  {
    hits = 0;
    misses = 0;
  }
}
