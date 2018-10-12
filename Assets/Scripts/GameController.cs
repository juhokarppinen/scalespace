using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  private enum GameState { Menu, PreLevel, Level };

  private Score[] scores;
  private Level[] levels;
  private Level level;
  private Swarm swarm;
  private GameState gameState = GameState.Menu;
  private Clock clock;
  private PlayerController playerController;
  private VisibilityToggle instructionDisplay;
  private VisibilityToggle titleDisplay;
  private VisibilityToggle meterDisplay;
  private VisibilityToggle musicMat;
  private InformationCanvas infoDisplay;
  private ScoreCanvas[] scoreDisplays;
  private Dictionary<string, int> hintDisplayTime;

  public bool IsInMenu { get { return gameState == GameState.Menu; } }

  void Awake()
  {
    clock = FindObjectOfType<Clock>();
    infoDisplay = FindObjectOfType<InformationCanvas>();
    instructionDisplay = FindObjectOfType<InstructionCanvas>();
    meterDisplay = FindObjectOfType<MeterCanvas>();
    musicMat = FindObjectOfType<MusicMat>().GetComponent<VisibilityToggle>();
    playerController = FindObjectOfType<PlayerController>();
    scoreDisplays = FindObjectsOfType<ScoreCanvas>();
    swarm = FindObjectOfType<Swarm>();
    titleDisplay = FindObjectOfType<TitleCanvas>();
  }

  void Start()
  {
    hintDisplayTime = new Dictionary<string, int>{
      {"1",5 },
      {"2",5 },
      {"3",5 },
      {"4",5 },
      {"5",5 },
      };
    scores = new[] { new Score(), new Score() };
    levels = new Level[] {
      new Level(1, new [] { "1", "5" }),
      new Level(2, new [] { "1", "4" }),
      new Level(3, new [] { "1", "3" }),
      new Level(4, new [] { "1", "2" }),
      new Level(5, new [] { "1", "3", "5" }),
      new Level(6, new [] { "1", "2", "5" }),
      new Level(7, new [] { "1", "4", "5" }),
      new Level(8, new [] { "1", "2", "3" }),
      new Level(9, new [] { "1", "3", "4" }),
      new Level(10, new [] { "1", "2", "3", "4", "5" }),
      };
  }

  void Update()
  {
    if (IsInMenu && Control.GetKeyDown(new[] { 3, 14 }, KeyCode.Space))
    {
      StartGame();
    }
  }

  public float GetHintDisplayTime(string forNote)
  {
    return hintDisplayTime[forNote];
  }

  public void StartGame()
  {
    SetLevel(1);
    playerController.Reset();
    foreach (var score in scores) score.Reset();

    foreach (var canvas in scoreDisplays) canvas.Hide();
    instructionDisplay.Hide();
    titleDisplay.Hide();
    meterDisplay.Show();
    musicMat.Show();
  }

  public void Hit(int player, string note)
  {
    DecreaseHintTime(note);
    scores[player - 1].Hit();
    if (swarm.CubeDestroyed() <= 0)
      SetLevel(level.levelNumber + 1);
  }

  public void Collide(int player, string note)
  {
    IncreaseHintTime(note);
    Miss(player);
  }

  public void Miss(int player)
  {
    scores[player - 1].Miss();
  }

  public void GameOver()
  {
    if (gameState == GameState.Menu) return;
    gameState = GameState.Menu;

    ClearRemainingNotes();
    swarm.DestroyAll();
    clock.Off();

    meterDisplay.Hide();
    musicMat.Hide();
    titleDisplay.Show();
    instructionDisplay.Show();
    foreach (var canvas in scoreDisplays)
    {
      var forPlayer = canvas.forPlayer - 1;
      canvas.SetHits(scores[forPlayer].Hits);
      canvas.SetMisses(scores[forPlayer].Misses);
      canvas.SetAccuracy(scores[forPlayer].Accuracy);
      canvas.Show();
    }
  }

  private void SetLevel(int levelNumber)
  {
    gameState = GameState.PreLevel;
    if (levelNumber > levels.Length)
    {
      GameOver();
      return;
    }

    ClearRemainingNotes();
    level = levels[levelNumber - 1];
    swarm.SetTargetAmount(level.target);
    foreach (var group in FindObjectsOfType<GeneratorGroup>()) group.SetNotes(level.notes);

    var delayBeforeStart = 4f;

    clock.Off();
    clock.tempo = level.tempo;
    clock.On(delayBeforeStart);

    infoDisplay.SetLevel(level.levelNumber);
    infoDisplay.SetNotes(level.notes);
    infoDisplay.Show();
    infoDisplay.Hide(delayBeforeStart);
  }

  private void StartLevel()
  {
    clock.On();
    infoDisplay.Hide();
  }

  private void ClearRemainingNotes()
  {
    foreach (var cube in GameObject.Find("Active Cubes").transform.GetComponentsInChildren<NoteCube>())
      cube.Explode();
  }

  private void IncreaseHintTime(string forNote)
  {
    hintDisplayTime[forNote] += 1;
    if (hintDisplayTime[forNote] > 5) hintDisplayTime[forNote] = 5;
  }

  private void DecreaseHintTime(string forNote)
  {
    hintDisplayTime[forNote] -= 1;
    if (hintDisplayTime[forNote] < 0) hintDisplayTime[forNote] = 0;
  }
}
