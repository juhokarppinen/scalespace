using UnityEngine;

public class Rotator : MonoBehaviour
{
  public Vector3 rotation;

  void Update()
  {
    transform.localRotation *= Quaternion.Euler(rotation);
  }
}
