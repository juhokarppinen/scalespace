using UnityEngine;
using MidiJack;

public static class Control
{
  public static bool GetKeyDown(int note, KeyCode keyCode)
  {
    return MidiMaster.GetKeyDown(note) || Input.GetKeyDown(keyCode);
  }

  public static bool GetKeyDown(int[] notes, KeyCode keyCode)
  {
    if (Input.GetKeyDown(keyCode)) return true;
    foreach (var note in notes)
      if (MidiMaster.GetKeyDown(note)) return true;
    return false;
  }

  public static bool GetKeyUp(int note, KeyCode keyCode)
  {
    return MidiMaster.GetKeyUp(note) || Input.GetKeyUp(keyCode);
  }
}
