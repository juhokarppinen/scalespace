using UnityEngine;

public class Beam : MonoBehaviour
{
  public float intensity = 3.5f;

  public void SetColor(Color color)
  {
    GetComponent<Renderer>().material.SetColor("_EmissionColor", color * intensity);
  }
}
