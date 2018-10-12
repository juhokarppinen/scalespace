using System.Collections;
using UnityEngine;

public class NoteCube : MonoBehaviour
{
  public string note = "1";
  public GameObject largeExplosion;
  public GameObject explosion;
  public GameObject explosionSound;

  private int forPlayer;
  private float movementSpeed;
  private Color color;
  private NoteCubeHalf[] halves;
  private TextMesh[] textMeshes;
  private Vector3 targetPosition;
  private Vector3 direction;

  public int Player { get { return forPlayer; } }

  void Awake()
  {
    halves = GetComponentsInChildren<NoteCubeHalf>();
    textMeshes = GetComponentsInChildren<TextMesh>();
  }

  void Start()
  {
    foreach (var half in halves) half.GetComponent<Renderer>().material.color = color;
    movementSpeed = (Vector3.Distance(transform.position, targetPosition)) / FindObjectOfType<Clock>().BarLength;
  }

  void Update()
  {
    transform.position += direction * Time.deltaTime * movementSpeed;
  }

  public void SetNote(string note)
  {
    this.note = note;
    foreach (var textMesh in textMeshes) textMesh.text = note;
  }

  public void SetColor(Color color)
  {
    this.color = color;
  }

  public void SetPlayer(int forPlayer)
  {
    this.forPlayer = forPlayer;
  }

  public void SetTargetPosition(Vector3 targetPosition)
  {
    this.targetPosition = targetPosition;
    transform.LookAt(targetPosition);
    transform.rotation = transform.rotation * Quaternion.Euler(0, 180f, 0);
    direction = (targetPosition - transform.position).normalized;
  }

  public void Explode()
  {
    FindObjectOfType<CameraManager>().ShakeSoft();
    Helpers.Instantiate(explosion, transform.position, Quaternion.identity, "Particle Effects");
    Helpers.Instantiate(explosionSound, transform.position, Quaternion.identity, "Sound Effects");
    halves[0].Activate(-1f);
    halves[1].Activate(1f);
    Destroy(gameObject);
  }

  public void CollideWithPlayer()
  {
    FindObjectOfType<GameController>().Collide(forPlayer, note);
    FindObjectOfType<PlayerController>().Health -= 20f;
    FindObjectOfType<CameraManager>().ShakeHard();
    Helpers.Instantiate(largeExplosion, transform.position, Quaternion.identity, "Particle Effects");
    Helpers.Instantiate(explosionSound, transform.position, Quaternion.identity, "Sound Effects");
    Destroy(gameObject);
  }

  public void HideNoteTexts(float time)
  {
    StartCoroutine(FadeNoteText(time));
  }

  private IEnumerator FadeNoteText(float target)
  {
    var elapsed = 0f;
    while (elapsed < target)
    {
      elapsed += Time.deltaTime;
      foreach (var textMesh in textMeshes)
      {
        var alpha = Mathf.SmoothStep(1f, 0f, elapsed / target);
        textMesh.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, alpha);
      }
      yield return null;
    }
    foreach (var textMesh in textMeshes)
    {
      Destroy(textMesh);
    }
  }
}
