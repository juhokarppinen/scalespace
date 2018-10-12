using UnityEngine;

public class NoteCubeGenerator : MonoBehaviour
{
  [Range(1, 2)]
  public int forPlayer = 1;
  public string note = "1";
  public GameObject noteCube;
  public GameObject particleEffect;
  public Vector3 targetPosition;

  private Color color;
  private AudioSource audioSource;
  private GameController gameController;

  void Awake()
  {
    audioSource = GetComponent<AudioSource>();
    gameController = FindObjectOfType<GameController>();
  }

  void Start()
  {
    color = FindObjectOfType<Constants>().GetColor(forPlayer);
  }

  public void Generate(string note)
  {
    NoteCube newCube = Instantiate(noteCube, transform.position, Quaternion.identity).GetComponent<NoteCube>();
    audioSource.clip = FindObjectOfType<AudioController>().GetGeneratorPitch(note);
    audioSource.Play();
    newCube.SetTargetPosition(targetPosition);
    newCube.SetNote(note);
    newCube.SetColor(color);
    newCube.SetPlayer(forPlayer);
    newCube.HideNoteTexts(gameController.GetHintDisplayTime(note));
    newCube.transform.parent = GameObject.Find("Active Cubes").transform;
    Helpers.Instantiate(particleEffect, transform.position, Quaternion.identity, "Particle Effects");
  }

  public void Generate()
  {
    Generate(note);
  }
}
