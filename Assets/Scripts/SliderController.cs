using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
  private Slider[] sliders;
  private Image[] backgrounds;
  private Color defaultBackground;

  public void SetWarning(bool warning)
  {
    foreach (var background in backgrounds)
      background.color = warning ? Color.red : defaultBackground;
  }

  public void SetValue(float value)
  {
    foreach (var slider in sliders)
      slider.value = value;
  }

  void Start()
  {
    backgrounds = new Image[2];
    sliders = GetComponentsInChildren<Slider>();
    backgrounds[0] = sliders[0].GetComponentInChildren<Image>();
    backgrounds[1] = sliders[1].GetComponentInChildren<Image>();
    defaultBackground = backgrounds[0].color;
  }
}
