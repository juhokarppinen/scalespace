using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
  public float maxHealth = 100f;
  public float maxEnergy = 100f;
  public float restorationRate = 3f;

  private float health;
  private float energy;
  private SliderController healthSlider;
  private SliderController energySlider;

  public float Health { get { return health; } set { SetHealth(value); } }
  public float Energy { get { return energy; } set { SetEnergy(value); } }

  void Awake()
  {
    health = maxHealth;
    energy = maxEnergy;
    healthSlider = FindObjectOfType<Health>();
    energySlider = FindObjectOfType<Energy>();
  }

  void Update()
  {
    var amount = restorationRate * Time.deltaTime;
    Health += amount / 3f;
    Energy += amount;
    healthSlider.SetWarning(health < 20f);
    energySlider.SetWarning(energy < 10f);
  }

  public void Reset()
  {
    SetEnergy(maxEnergy);
    SetHealth(maxHealth);
  }

  private void SetHealth(float amount)
  {
    health = amount <= maxHealth ? amount : maxHealth;
    healthSlider.SetValue(health / maxHealth);
    if (health <= 0f)
    {
      FindObjectOfType<GameController>().GameOver();
      Reset();
    }
  }

  private void SetEnergy(float amount)
  {
    energy = amount <= maxEnergy ? amount : maxEnergy;
    energySlider.SetValue(energy / maxEnergy);
  }
}
