using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Animator animator;
    [SerializeField] private Vector3 posicionPorDefecto = new Vector3(-5.6f, -1.4f, 0f);
    private Rigidbody2D rb;
    private bool canJump;
    private bool canDoubleJump;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public AudioClip jumpSound;
    public AudioClip deathSound;
    [SerializeField] private AudioClip footstepSound;

    private AudioSource audioSource;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public AudioClip attackSound;

    void Start()
    {
        PlayerPrefs.DeleteAll();

        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        rb.isKinematic = true;
        RespawnPlayer();
        rb.isKinematic = false;
        rb.velocity = Vector2.zero;
    }

    void Update()
    {
        float moveX = 0f;

        bool isCrouching = Input.GetKey(KeyCode.S);
        animator.SetBool("isCrouching", isCrouching);

        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        else if (Input.GetKey(KeyCode.D)) moveX = 1f;

        // Solo mover si no está quieto y agachado
        if (!isCrouching || moveX != 0)
            rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);

        animator.SetFloat("Movement", Mathf.Abs(moveX));

        // Comprobación de salto
        canJump = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("canJump", canJump);

        if (canJump) canDoubleJump = true;

        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            audioSource.PlayOneShot(jumpSound);
            animator.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.W) && !canJump && canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = false;
            audioSource.PlayOneShot(jumpSound);
            animator.SetTrigger("Jump");
        }

        // Girar sprite
        if (moveX != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveX) * 0.7f, 0.7f, 0.7f);

        // Atacar
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack();
        }

        // Caída
        if (transform.position.y <= -12.68f)
        {
            GameManager.Instance.LoseHealth();
            audioSource.PlayOneShot(deathSound);
            RespawnPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeadZone"))
        {
            audioSource.PlayOneShot(deathSound);
            GameManager.Instance.LoseHealth();
            RespawnPlayer();
        }

        if (other.CompareTag("Checkpoint"))
        {
            PlayerPrefs.SetFloat("CheckpointX", transform.position.x);
            PlayerPrefs.SetFloat("CheckpointY", transform.position.y);
            PlayerPrefs.SetInt("HasCheckpoint", 1);
            PlayerPrefs.Save();
        }
    }

    public void RespawnPlayerExternamente()
    {
        RespawnPlayer();
    }

    private void RespawnPlayer()
    {
        if (PlayerPrefs.HasKey("HasCheckpoint") && PlayerPrefs.GetInt("HasCheckpoint") == 1)
        {
            float x = PlayerPrefs.GetFloat("CheckpointX");
            float y = PlayerPrefs.GetFloat("CheckpointY");
            transform.position = new Vector3(x, y, transform.position.z);
        }
        else
        {
            GameObject spawnPoint = GameObject.Find("SpawnPoint");
            if (spawnPoint != null)
            {
                transform.position = spawnPoint.transform.position;
            }
            else
            {
                transform.position = posicionPorDefecto;
            }
        }

        rb.velocity = Vector2.zero;
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Debug.Log("Atacando..."); // <-- Verifica si esto aparece en consola

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Enemigo detectado: " + enemy.name); // <-- Verifica si detecta al enemigo

            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage();
            }

            
        }
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    // Método para activar animación de golpe
    public void GetHit()
    {
        animator.SetTrigger("GetHit");
    }
}
