using System.Collections;
using UnityEngine;

public class Blinker : MonoBehaviour
{
  [Range(1, 2)]
  public int forPlayer = 1;

  public int note;
  public KeyCode keyCode;
  public GameObject beam;
  public float activeLightIntensity = 10f;

  private bool isActive;
  private float fadeTime = 1.0f;
  private float timeElapsed = 0f;
  private float activeSize = 1.5f;
  private Color activeColor;
  private Color inactiveColor;
  private Material material;
  private Sound sound;
  private Vector3 defaultScale;
  private Light lightEmitter;
  private PlayerController player;

  private string Pitch
  {
    get { return GetComponentInChildren<Sound>().pitch; }
  }

  void Awake()
  {
    material = GetComponent<Renderer>().material;
    sound = GetComponent<Sound>();
    lightEmitter = GetComponentInChildren<Light>();
    defaultScale = transform.localScale;
    activeColor = FindObjectOfType<Constants>().GetColor(forPlayer);
    inactiveColor = material.color;
    player = FindObjectOfType<PlayerController>();
  }

  void Update()
  {
    if (Control.GetKeyDown(note, keyCode)) Activate();
    if (Control.GetKeyUp(note, keyCode)) Deactivate();
    if (!isActive) Normalize();
  }

  private void Activate()
  {
    if (player.Energy > 10f)
    {
      isActive = true;
      material.color = activeColor;
      lightEmitter.intensity = activeLightIntensity;
      transform.localScale = Vector3.one * activeSize;
      sound.Play();
      if (beam != null)
      {
        FindObjectOfType<PlayerController>().Energy -= FindObjectOfType<GameController>().IsInMenu ? 0 : 10f;
        FindAndDestroyTarget();
      }
    }
    else
    {
      // TODO: Play buzz, flash light etc.
    }
  }

  private void FindAndDestroyTarget()
  {
    var cubes = FindObjectsOfType<NoteCube>();
    var gameController = FindObjectOfType<GameController>();
    NoteCube correctCube = null;
    foreach (var cube in cubes)
    {
      if (cube.note.ToString() == Pitch && cube.Player == forPlayer)
      {
        correctCube = cube;
      }
    }
    var targetPosition = new Vector3(0f, 25f, 100f);
    if (correctCube != null)
    {
      gameController.Hit(forPlayer, correctCube.note);
      targetPosition = correctCube.transform.position;
      correctCube.Explode();
    }
    else
    {
      gameController.Miss(forPlayer);
    }
    var newBeam = Instantiate(beam, transform.position, Quaternion.identity).GetComponent<LineRenderer>();
    newBeam.SetPositions(new Vector3[] { transform.position, targetPosition });
    newBeam.GetComponent<Beam>().SetColor(activeColor);
  }

  public void Activate(float delay)
  {
    Activate();
    StartCoroutine(DelayedDeactivate(delay));
  }

  private void Deactivate()
  {
    isActive = false;
    timeElapsed = 0f;
  }

  private void Normalize()
  {
    timeElapsed += Time.deltaTime;
    material.color = Color.Lerp(material.color, inactiveColor, timeElapsed / fadeTime);
    lightEmitter.intensity = Mathf.Lerp(lightEmitter.intensity, 0f, timeElapsed / fadeTime);
    transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, timeElapsed / fadeTime);
  }

  private IEnumerator DelayedDeactivate(float delay)
  {
    while (delay >= 0f)
    {
      delay -= Time.deltaTime;
      yield return null;
    }
    Deactivate();
  }
}
