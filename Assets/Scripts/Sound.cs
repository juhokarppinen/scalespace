using UnityEngine;

public class Sound : MonoBehaviour
{
  public string pitch;
  public float volume = 1f;

  private AudioClip clip;

  public void Play()
  {
    AudioHelper.PlayClipAtPoint(clip, transform.position, volume).transform.parent = GameObject.Find("Sound Effects").transform;
  }

  public void SetPitch(string note)
  {
    pitch = note;
    clip = FindObjectOfType<AudioController>().GetPitch(note);
  }

  void Start()
  {
    if (pitch != "" && clip == null) clip = FindObjectOfType<AudioController>().GetPitch(pitch);
  }
}
