using System.Collections;
using UnityEngine;

public class Clock : MonoBehaviour
{
  public int tempo = 120;
  public int beatsPerBar = 4;
  public float Interval { get { return 60f / (tempo * 1f); } }
  public float HalfInterval { get { return Interval / 2f; } }
  public float BarLength { get { return Interval * beatsPerBar; } }

  private bool running;
  private bool offBeat;
  private int beat;
  private float elapsed;

  public delegate void BeatAction();
  public static event BeatAction DownBeat;
  public static event BeatAction BackBeat;
  public static event BeatAction OnBeat;
  public static event BeatAction OffBeat;

  void Awake()
  {
    Reset();
  }

  void Update()
  {
    if (running)
    {
      elapsed += Time.deltaTime;
      if (elapsed >= HalfInterval)
      {
        elapsed = 0f;
        Tick();
      }
    }
  }

  public void On()
  {
    Reset();
    running = true;
  }

  public void On(float delay)
  {
    StartCoroutine(DelayedOn(delay));
  }

  public void Off()
  {
    running = false;
  }

  private void Tick()
  {
    offBeat = !offBeat;
    if (offBeat && OffBeat != null)
    {
      OffBeat();
    }
    else
    {
      beat += 1;
      if (beat > beatsPerBar)
        beat = 1;
      if (OnBeat != null)
        OnBeat();
      if (beat == 1 && DownBeat != null)
        DownBeat();
      if (beat == 3 && BackBeat != null)
        BackBeat();
    }
  }

  private IEnumerator DelayedOn(float delay)
  {
    var elapsed = 0f;
    while (elapsed < delay)
    {
      elapsed += Time.deltaTime;
      yield return null;
    }
    On();
  }

  private void Reset()
  {
    running = false;
    offBeat = true;
    beat = 4;
    elapsed = HalfInterval;
  }
}
