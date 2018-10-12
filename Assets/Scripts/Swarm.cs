using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour
{
  public GameObject noteCube;
  public float swarmSpeed = 1f;
  public Vector3 displacement;
  public Vector3 ratio = new Vector3(1f, 1f, 1f);

  private float twoPi = Mathf.PI * 2;
  private float angle;
  private float distance;
  private List<DeadCube> noteCubes;

  private int Count { get { return noteCubes.Count; } }

  public void SetTargetAmount(int amount)
  {
    for (var i = 0; i < amount; i++)
    {
      var newCube = Instantiate(noteCube, transform.position, Quaternion.identity);
      newCube.transform.parent = transform;
      noteCubes.Add(newCube.GetComponent<DeadCube>());
    }
    distance = twoPi / Count;
  }

  public int CubeDestroyed()
  {
    DestroyCube();
    if (Count > 1) StartCoroutine(ChangeDistance());
    return Count;
  }

  public void DestroyAll()
  {
    StartCoroutine(DelayedDestroy());
  }

  private void DestroyCube()
  {
    noteCubes[Count - 1].Explode();
    noteCubes.RemoveAt(Count - 1);
  }

  private IEnumerator DelayedDestroy()
  {
    if (Count == 0) yield break;
    var elapsed = 0f;
    var target = .15f;
    while (elapsed < target)
    {
      elapsed += Time.deltaTime;
      yield return null;
    }
    DestroyCube();
    StartCoroutine(DelayedDestroy());
  }

  void Awake()
  {
    noteCubes = new List<DeadCube>();
  }

  void Update()
  {
    angle += Time.deltaTime * swarmSpeed;
    if (angle > 12 * twoPi) angle -= 12 * twoPi;
    for (var i = 0; i < Count; i++)
    {
      var step = distance * i;
      var x = Mathf.Sin(angle / ratio.x + step) * displacement.x;
      var y = Mathf.Sin(angle / ratio.y + step) * displacement.y;
      var z = Mathf.Sin(angle / ratio.z + step) * displacement.z;
      noteCubes[i].transform.localPosition = new Vector3(x, y, z);
    }
  }

  private IEnumerator ChangeDistance()
  {
    var elapsed = 0f;
    var target = 1f;
    var currentDistance = distance;
    var newDistance = twoPi / Count;
    while (elapsed <= target)
    {
      elapsed += Time.deltaTime;
      distance = Mathf.SmoothStep(currentDistance, newDistance, elapsed / target);
      yield return null;
    }
  }
}
