using UnityEngine;

public static class Helpers
{
  public static GameObject Instantiate(GameObject gameObject, Vector3 position, Quaternion rotation, string parent)
  {
    var newObject = GameObject.Instantiate(gameObject, position, rotation);
    newObject.transform.parent = GameObject.Find(parent).transform;
    return newObject;
  }

  public static float RandMag(float magnitude)
  {
    return magnitude / 2f - Random.value * magnitude;
  }
}
