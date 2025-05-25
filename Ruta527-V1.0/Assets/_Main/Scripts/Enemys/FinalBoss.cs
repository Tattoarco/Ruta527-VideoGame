using UnityEngine;
using UnityEngine.UI;

public class FinalBoss : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public Slider healthBar;

    public Transform pointA;
    public Transform pointB;
    private Transform currentTarget;

    private Transform player;
    public float speed = 2f;
    public float detectDistance = 5f;

    [Header("Ground Check")]
    public Transform groundCheck;           // Punto para chequear el suelo (coloca un empty en pies)
    public float groundCheckRadius = 0.2f;  // Radio del círculo para detectar suelo
    public LayerMask groundLayer;            // Capa del suelo
    private bool isGrounded;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentTarget = pointB; // Empieza yendo a pointB
    }

    private void Update()
    {
        // Actualiza si está tocando el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Debug.Log("IsGrounded: " + isGrounded);

        if (!isGrounded)
            return; // No moverse si no está en el suelo

        if (player != null && Vector3.Distance(transform.position, player.position) < detectDistance)
        {
            // Perseguir al jugador si está cerca
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            // Patrullar entre A y B
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
            {
                // Cambia de destino al llegar
                currentTarget = currentTarget == pointA ? pointB : pointA;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Opcional: dibuja el círculo del groundCheck en el editor para facilitar la configuración
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
