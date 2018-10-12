using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
  public AudioClip[] pitches = new AudioClip[25];
  public AudioClip[] generatorPitches = new AudioClip[5];

  private AudioClip empty;
  private Dictionary<string, int> pitchMap = new Dictionary<string, int>
  {
    { "1", 0 },
    { "b2", 1 },
    { "2", 2 },
    { "b3", 3 },
    { "3", 4 },
    { "4", 5 },
    { "#4", 6 },
    { "b5", 6 },
    { "5", 7 },
    { "#5", 8 },
    { "b6", 8 },
    { "6", 9 },
    { "b7", 10 },
    { "7", 11 },
    { "8", 12 },
    { "b9", 13 },
    { "9", 14 },
    { "b10", 15 },
    { "10", 16 },
    { "11", 17 },
    { "#11", 18 },
    { "b12", 18 },
    { "12", 19 },
    { "#12", 20 },
    { "b13", 20 },
    { "13", 21 },
    { "b14", 22 },
    { "14", 23 },
    { "15", 24 },
  };
  public Dictionary<string, string> noteMap = new Dictionary<string, string>
  {
    { "1",  "C" },
    { "b2", "Des" },
    { "2",  "D" },
    { "b3", "Es" },
    { "3",  "E" },
    { "4",  "F" },
    { "#4", "Fis" },
    { "b5", "Ges" },
    { "5",  "G" },
    { "#5", "Gis" },
    { "b6", "As" },
    { "6",  "A" },
    { "b7","Bes" },
    { "7",  "B" },
    { "8",  "C" },
    { "b9", "Des" },
    { "9",  "D" },
    { "b10", "Es" },
    { "10", "E" },
    { "11", "F" },
    { "#11", "Fis" },
    { "b12", "Ges" },
    { "12", "G" },
    { "#12", "Gis" },
    { "b13", "As" },
    { "13", "A" },
    { "b14", "Bes" },
    { "14", "B" },
    { "15", "C" },
  };
  private Dictionary<string, int> generatorPitchMap = new Dictionary<string, int>
  {
    { "1", 0 },
    { "2", 1 },
    { "3", 2 },
    { "4", 3 },
    { "5", 4 },
  };

  void Awake()
  {
    empty = AudioClip.Create("Undefined", 1, 2, 44100, false);
    for (int i = 0; i < pitches.Length; i += 1)
    {
      if (pitches[i]) continue;
      pitches[i] = empty;
    }
  }

  public AudioClip GetPitch(string note)
  {
    string key = note.ToLower();

    if (pitchMap.ContainsKey(key))
    {
      return pitches[pitchMap[key]];
    }

    Debug.LogWarning("Unknown pitch: " + note);
    return empty;
  }

  public AudioClip GetGeneratorPitch(string note)
  {
    string key = note.ToLower();

    if (generatorPitchMap.ContainsKey(key))
    {
      return generatorPitches[generatorPitchMap[key]];
    }

    Debug.LogWarning("Unknown pitch: " + note);
    return empty;
  }
}
