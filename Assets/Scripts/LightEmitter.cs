using UnityEngine;

public class LightEmitter : MonoBehaviour
{
  public void SetIntensity(float intensity)
  {
    GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, intensity);
  }
}
