using UnityEngine;

public class MusicMat : MonoBehaviour
{
  public bool flashOnBeat = true;

  private Color defaultColor;
  private Material material;
  private Transform childTransform;
  private Vector3 defaultPosition;
  private float beatDisplacement = .1f;
  private Clock clock;

  void Awake()
  {
    childTransform = transform.GetChild(0).transform;
    defaultPosition = childTransform.localPosition;
    material = GetComponentInChildren<Renderer>().material;
    defaultColor = material.color;
    clock = FindObjectOfType<Clock>();
  }

  void Update()
  {
    var time = Time.deltaTime * (1 / clock.HalfInterval);
    material.color = Color.Lerp(material.color, defaultColor, time);
    childTransform.localPosition = Vector3.Lerp(childTransform.localPosition, defaultPosition, time);
  }

  void OnEnable()
  {
    var constants = FindObjectOfType<Constants>();
    Clock.OnBeat += delegate { Flash(constants.onbeatColor); };
    Clock.DownBeat += delegate { Flash(constants.downbeatColor); };
  }

  private void Flash(Color color)
  {
    material.color = color;
    childTransform.localPosition = defaultPosition + new Vector3(0, 0, 1f) * beatDisplacement;
  }
}
