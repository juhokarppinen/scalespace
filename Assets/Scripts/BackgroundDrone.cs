using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDrone : MonoBehaviour
{
  public float fadeInTime = 2f;
  private AudioSource audioSource;
  private float maxVolume;

  void Awake()
  {
    audioSource = GetComponent<AudioSource>();
    maxVolume = audioSource.volume;
  }

  void Start()
  {
    StartCoroutine(FadeIn());
  }

  private IEnumerator FadeIn()
  {
    var elapsed = 0f;
    while (elapsed < fadeInTime)
    {
      elapsed += Time.deltaTime;
      audioSource.volume = Mathf.Lerp(0f, maxVolume, elapsed / fadeInTime);
      yield return null;
    }
  }
}
