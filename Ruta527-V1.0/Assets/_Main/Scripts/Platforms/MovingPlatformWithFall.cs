using UnityEngine;
using System.Collections;

public class MovingPlatformWithFall : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float maxTimeWithPlayer = 2f;
    public float fallDelay = 1f;
    public float resetDelay = 3f; // Tiempo antes de volver a la posición inicial

    private Vector3 currentTarget;
    private float timeWithPlayer = 0f;
    private bool playerOnPlatform = false;
    private bool isFalling = false;
    private bool isWaiting = false;
    private Rigidbody2D rb;
    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.gravityScale = 0f;

        initialPosition = transform.position;

        if (pointA == null || pointB == null)
        {
            Debug.LogError("Please assign PointA and PointB in the Inspector!");
            enabled = false;
            return;
        }

        currentTarget = pointB.position;
    }

    void Update()
    {
        if (isFalling || isWaiting || pointA == null || pointB == null) return;

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
        {
            StartCoroutine(WaitBeforeSwitchTarget());
        }

        if (playerOnPlatform && !isFalling)
        {
            timeWithPlayer += Time.deltaTime;

            if (timeWithPlayer >= maxTimeWithPlayer)
            {
                StartCoroutine(Fall());
            }
        }
        else
        {
            timeWithPlayer = 0f;
        }
    }

    private IEnumerator WaitBeforeSwitchTarget()
    {
        isWaiting = true;
        yield return new WaitForSeconds(0.3f); // Espera 1 segundo
        currentTarget = currentTarget == pointA.position ? pointB.position : pointA.position;
        isWaiting = false;
    }

    private IEnumerator Fall()
    {
        isFalling = true;
        yield return new WaitForSeconds(fallDelay);

        rb.isKinematic = false;
        rb.gravityScale = 1f;

        yield return new WaitForSeconds(resetDelay);

        StartCoroutine(ResetPlatform());
    }

    private IEnumerator ResetPlatform()
    {
        // Espera a que la plataforma toque el suelo
        yield return new WaitForFixedUpdate();

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        rb.gravityScale = 0f;

        transform.position = initialPosition;
        currentTarget = pointB.position;
        isFalling = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isFalling)
        {
            playerOnPlatform = true;
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerOnPlatform = false;
            collision.collider.transform.SetParent(null);
        }
    }
}
