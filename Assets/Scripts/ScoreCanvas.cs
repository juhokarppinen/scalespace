using UnityEngine;
using UnityEngine.UI;

public class ScoreCanvas : VisibilityToggle
{
  [Range(1, 2)]
  public int forPlayer = 1;

  void Start()
  {
    foreach (var text in GetComponentsInChildren<Text>())
      text.color = FindObjectOfType<Constants>().GetColor(forPlayer);
  }

  public void SetHits(int hits)
  {
    SetValue("Hits", hits.ToString());
  }

  public void SetMisses(int misses)
  {
    SetValue("Misses", misses.ToString());
  }

  public void SetAccuracy(int accuracy)
  {
    SetValue("Accuracy", accuracy + "%");
  }

  private void SetValue(string target, string text)
  {
    transform.Find(target).GetChild(0).GetComponent<Text>().text = text;
  }
}
