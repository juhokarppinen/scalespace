using UnityEngine;

public static class AudioHelper
{
  public static GameObject PlayClipAtPoint(AudioClip clip, Vector3 position, float volume)
  {
    GameObject audioGameObject = new GameObject("One Shot Audio");
    audioGameObject.transform.position = position;
    AudioSource audio = audioGameObject.AddComponent<AudioSource>();
    audio.clip = clip;
    audio.volume = volume;
    audio.Play();
    GameObject.Destroy(audioGameObject, clip.length);
    return audioGameObject;
  }
}
