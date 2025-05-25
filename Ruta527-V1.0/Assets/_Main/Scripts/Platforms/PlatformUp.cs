using UnityEngine;

public class PlatformUp : MonoBehaviour
{
    [SerializeField] private Transform point; // Punto al que cae
    [SerializeField] private float speed = 2f;

    private bool shouldMove = false;
    private Vector3 startPosition;
    private bool isFalling = false;
    private bool hasDamagedPlayer = false;

    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (shouldMove && point != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, point.position) < 0.1f && !isFalling)
            {
                isFalling = true;
                StartCoroutine(ResetPlatformAfterDelay(2f));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shouldMove && collision.gameObject.CompareTag("Player"))
        {
            shouldMove = true; // Empieza a moverse al tocar al jugador
        }

        if (isFalling && !hasDamagedPlayer && collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // Verifica que el jugador esté debajo
                if (contact.normal.y > 0.5f)
                {
                    GameManager.Instance.LoseHealth();
                    hasDamagedPlayer = true;

                    if (audioSource != null && audioSource.clip != null)
                    {
                        audioSource.Play(); // Reproduce el sonido
                    }

                    Debug.Log("¡El jugador fue aplastado por la plataforma!");
                    break;
                }
            }
        }
    }

    System.Collections.IEnumerator ResetPlatformAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = startPosition;
        shouldMove = false;
        isFalling = false;
        hasDamagedPlayer = false;
    }
}
