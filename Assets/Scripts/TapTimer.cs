using UnityEngine;

public class TapTimer : MonoBehaviour
{
  [Range(0f, 0.25f)]
  public float threshold = 0.1f;

  private bool playerHasTapped = false;
  private float elapsed = 0f;
  private float delta = 0f;
  private Clock clock;

  public float Delta { get { return delta; } }

  void Awake()
  {
    clock = FindObjectOfType<Clock>();
  }

  void Update()
  {
    elapsed += Time.deltaTime;
    if (Control.GetKeyDown(0, KeyCode.A))
      PlayerTapped();
  }

  void OnEnable()
  {
    Clock.OnBeat += OnBeat;
    Clock.OffBeat += OffBeat;
  }

  private void OnBeat()
  {
    elapsed = 0f;
  }

  private void OffBeat()
  {
    elapsed = -clock.HalfInterval;
    playerHasTapped = false;
  }

  private void PlayerTapped()
  {
    if (playerHasTapped) return;
    playerHasTapped = true;
    delta = elapsed;
    if (Mathf.Abs(delta) <= threshold)
    {
      Debug.Log("HIT " + delta);
    }
    else if (delta < 0)
    {
      Debug.Log("EARLY " + delta);
    }
    else
    {
      Debug.Log("LATE " + delta);
    }
  }
}
