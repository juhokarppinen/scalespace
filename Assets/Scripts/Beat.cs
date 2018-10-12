using UnityEngine;

public class Beat : MonoBehaviour
{
  public GameObject kickSound;
  public GameObject bassSound;

  private AudioSource kick;
  private AudioSource bass;

  void Awake()
  {
    kick = kickSound.GetComponent<AudioSource>();
    bass = bassSound.GetComponent<AudioSource>();
  }

  void OnEnable()
  {
    Clock.DownBeat += OnBeat;
    Clock.OnBeat += OnBeat;
    Clock.OffBeat += OffBeat;
  }

  void OnDisable()
  {
    Clock.DownBeat -= OnBeat;
    Clock.OnBeat -= OnBeat;
    Clock.OffBeat -= OffBeat;
  }

  private void OnBeat()
  {
    kick.Play();
    bass.Stop();
    bass.time = 0;
  }

  private void OffBeat()
  {
    bass.Play();
    kick.Stop();
    kick.time = 0;
  }
}
