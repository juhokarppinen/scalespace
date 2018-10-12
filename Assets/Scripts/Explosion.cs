using UnityEngine;

public class Explosion : MonoBehaviour
{
  public void SetColor(Color color)
  {
    GetComponent<Renderer>().sharedMaterial.color = color;
  }
}
