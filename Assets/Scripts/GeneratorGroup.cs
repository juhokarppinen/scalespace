using UnityEngine;

public class GeneratorGroup : MonoBehaviour
{
  public bool generateEveryOtherBar = true;
  public bool generateOnBackBeat = false;
  public bool generateOnNextDownbeat = true;
  private NoteCubeGenerator[] generators;
  private string[] notes;

  void OnEnable()
  {
    if (generateOnBackBeat)
      Clock.BackBeat += Generate;
    else
      Clock.DownBeat += DownBeat;
  }

  void Awake()
  {
    generators = GetComponentsInChildren<NoteCubeGenerator>();
  }

  public void SetNotes(string[] notes)
  {
    this.notes = notes;
  }

  private void DownBeat()
  {
    if (generateOnNextDownbeat) Generate();
    generateOnNextDownbeat = !generateOnNextDownbeat;
  }

  private void Generate()
  {
    var note = NextNote();
    generators[RandomIndex()].Generate(note.ToString());
  }

  private string NextNote()
  {
    return notes[RandomIndex()];
  }

  private int RandomIndex()
  {
    return (int)(Random.value * notes.Length);
  }
}
