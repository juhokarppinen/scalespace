using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCubeHalf : MonoBehaviour
{
  public float lifetime;
  public GameObject explosion;
  public GameObject explosionSound;

  private LightEmitter lightEmitter;

  void Awake()
  {
    lightEmitter = GetComponentInChildren<LightEmitter>();
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      var cube = GetComponentInParent<NoteCube>();
      if (cube != null) cube.CollideWithPlayer();
    }
  }

  public void Activate(float direction)
  {
    transform.parent = null;
    var rb = GetComponent<Rigidbody>();
    rb.isKinematic = false;
    rb.AddForce(new Vector3(direction * 15f, Helpers.RandMag(15f), 0), ForceMode.VelocityChange);
    rb.AddTorque(new Vector3(Helpers.RandMag(100f), Helpers.RandMag(100f), Helpers.RandMag(200f)));
    StartCoroutine(SelfDestruct());
  }

  private IEnumerator SelfDestruct()
  {
    float elapsed = 0f;
    float target = 1f;
    while (elapsed < target)
    {
      elapsed += Time.deltaTime;
      lightEmitter.SetIntensity(Mathf.Lerp(1f, 0f, elapsed / target));
      yield return null;
    }
    Helpers.Instantiate(explosion, transform.position, transform.rotation, "Particle Effects");
    Helpers.Instantiate(explosionSound, transform.position, Quaternion.identity, "Sound Effects");
    Destroy(gameObject);
  }
}
