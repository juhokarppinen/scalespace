using System.Collections;
using UnityEngine;

public class VisibilityToggle : MonoBehaviour
{
  public bool initiallyVisible;
  public float moveTime = 1f;
  public Vector3 invisiblePosition;

  private Vector3 visiblePosition;

  void Awake()
  {
    visiblePosition = transform.localPosition;
    transform.localPosition = initiallyVisible ? transform.localPosition : invisiblePosition;
  }

  public void Show()
  {
    StartCoroutine(Move(visiblePosition));
  }

  public void Hide()
  {
    StartCoroutine(Move(invisiblePosition));
  }

  public void Hide(float delay)
  {
    StartCoroutine(MoveWithDelay(invisiblePosition, delay));
  }

  protected IEnumerator Move(Vector3 targetPosition)
  {
    var elapsed = 0f;
    while (elapsed < moveTime)
    {
      elapsed += Time.deltaTime;
      transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, elapsed / moveTime);
      yield return null;
    }
  }

  protected IEnumerator MoveWithDelay(Vector3 targetPosition, float delay)
  {
    var elapsed = 0f;
    while (elapsed < delay)
    {
      elapsed += Time.deltaTime;
      yield return null;
    }
    StartCoroutine(Move(targetPosition));
  }
}
