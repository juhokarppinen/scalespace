using UnityEngine;

public class ColorPulse : MonoBehaviour
{
  public float speed = 1f;

  private Color color1;
  private Color color2;
  private float angle;

  void Awake()
  {
    angle = Random.value * 2f * Mathf.PI;
    color1 = FindObjectOfType<Constants>().GetColor(1);
    color2 = FindObjectOfType<Constants>().GetColor(2);
  }

  void Update()
  {
    var addition = Time.deltaTime * speed;
    angle = angle >= 2f * Mathf.PI ? addition : angle + addition;
    var value = (Mathf.Sin(angle) + 1f) / 2f;
    GetComponent<Renderer>().material.color = Color.Lerp(color1, color2, value);
  }
}
