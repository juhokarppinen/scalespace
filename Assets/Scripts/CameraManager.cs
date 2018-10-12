using UnityEngine;

public class CameraManager : MonoBehaviour
{
  private EZCameraShake.CameraShaker shaker;

  void Awake()
  {
    shaker = FindObjectOfType<EZCameraShake.CameraShaker>();
  }

  void Start()
  {
    shaker.Shake(EZCameraShake.CameraShakePresets.HandheldCamera);
  }

  public void ShakeSoft()
  {
    shaker.Shake(EZCameraShake.CameraShakePresets.Bump);
  }

  public void ShakeHard()
  {
    shaker.Shake(EZCameraShake.CameraShakePresets.Explosion);
  }
}
