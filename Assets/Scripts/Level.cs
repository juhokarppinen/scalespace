public class Level
{
  public int levelNumber;
  public string[] notes;
  public int tempo;
  public int target;

  public Level(int levelNumber, string[] notes)
  {
    this.levelNumber = levelNumber;
    this.notes = notes;
    this.tempo = 60 + levelNumber * 5;
    this.target = 1 + levelNumber * 5;
  }
}
