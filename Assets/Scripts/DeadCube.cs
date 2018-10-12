using UnityEngine;

public class DeadCube : MonoBehaviour
{
  public GameObject explosion;

  public void Explode()
  {
    Helpers.Instantiate(explosion, transform.position, Quaternion.identity, "Particle Effects");
    Destroy(gameObject);
  }
}
