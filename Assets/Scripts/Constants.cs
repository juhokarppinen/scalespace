using UnityEngine;

public class Constants : MonoBehaviour
{
  public Color[] playerColors;
  [ColorUsageAttribute(true, true)]
  public Color downbeatColor;
  [ColorUsageAttribute(true, true)]
  public Color onbeatColor;

  public Color GetColor(int player)
  {
    return playerColors[player - 1];
  }
}
