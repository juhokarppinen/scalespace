using System.Collections;
using UnityEngine;

public class Fadeout : MonoBehaviour
{
  public float fadeDuration;
  private Color startColor;
  private Color endColor;

  public void SetFadeDuration(float duration)
  {
    fadeDuration = duration;
  }

  void Awake()
  {
    startColor = GetComponent<Renderer>().material.color;
    endColor = startColor - new Color(0, 0, 0, 1f);
  }

  void Start()
  {
    StartCoroutine(Dim());
  }

  private IEnumerator Dim()
  {
    var elapsed = 0f;
    while (elapsed < fadeDuration)
    {
      elapsed += Time.deltaTime;
      GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, elapsed / fadeDuration);
      yield return null;
    }
    Destroy(gameObject);
  }
}
