using UnityEngine;

public class OneShotAudioSource : MonoBehaviour
{
  private AudioSource audioSource;
  private float duration;
  private float elapsed;

  void Awake()
  {
    audioSource = GetComponent<AudioSource>();
    duration = audioSource.clip.length;
  }

  void Update()
  {
    elapsed += Time.deltaTime;
    if (elapsed >= duration)
    {
      Destroy(gameObject);
    }
  }
}
