using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationCanvas : VisibilityToggle
{
  public void SetLevel(int level)
  {
    transform.Find("Level").GetComponent<Text>().text = "Level " + level;
  }

  public void SetNotes(string[] notes)
  {
    ResetNotes();
    foreach (var note in notes)
      transform.Find(note).GetComponent<Text>().color = new Color(1f, 1f, 1f, .5f);
  }

  public void HitNote(string note)
  {
    var text = transform.Find(note).GetComponent<Text>();
    var alpha = text.color.a + .25f;
    text.color = new Color(1f, 1f, 1f, alpha);
    if (alpha >= 1f)
    {

    }
  }

  private void ResetNotes()
  {
    for (var i = 1; i < transform.childCount; i++)
    {
      transform.GetChild(i).GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.1f);
    }
  }
}
