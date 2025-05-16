using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool canJump;
    private bool canDoubleJump;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public AudioClip jumpSound;
    public AudioClip fallSound;
    public AudioClip deathSound;
    private AudioSource audioSource;
    [SerializeField] private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        rb.isKinematic = true;

        // ✅ Solo si ya tiene un checkpoint guardado, se respawnea ahí
        if (PlayerPrefs.HasKey("HasCheckpoint") && PlayerPrefs.GetInt("HasCheckpoint") == 1)
        {
            float x = PlayerPrefs.GetFloat("CheckpointX");
            float y = PlayerPrefs.GetFloat("CheckpointY");
            transform.position = new Vector3(x, y, transform.position.z);
        }
        else
        {
            // Posición inicial del juego
            transform.position = new Vector3(-5.31f, -4.13f, transform.position.z);
        }

        rb.isKinematic = false;
        rb.velocity = Vector2.zero;
    }

    void Update()
    {
        float moveX = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -0.7f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 0.7f;
        }

        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        canJump = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("canJump", canJump);
        animator.SetFloat("Movement", canJump ? Mathf.Abs(moveX) : 0f);

        if (canJump)
        {
            canDoubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            audioSource.PlayOneShot(jumpSound);
        }

        if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.W) && !canJump && canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = false;
            audioSource.PlayOneShot(jumpSound);
        }

        if (moveX > 0)
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        else if (moveX < 0)
        {
            transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
        }

        if (transform.position.y <= -12.68f)
        {
            RespawnPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            audioSource.PlayOneShot(deathSound);
            RespawnPlayer();
        }

        // ✅ Guardar checkpoint si pasa por uno
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            PlayerPrefs.SetFloat("CheckpointX", transform.position.x);
            PlayerPrefs.SetFloat("CheckpointY", transform.position.y);
            PlayerPrefs.SetInt("HasCheckpoint", 1); // ← Marcar que ya activó un checkpoint
            PlayerPrefs.Save();
        }
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
            // Posición de inicio si no hay checkpoint
            transform.position = new Vector3(-5.31f, -2.93f, transform.position.z);
        }

        rb.velocity = Vector2.zero;
    }

    // ✅ Llamar este método si quieres borrar el progreso (por ejemplo, desde un botón de "Nueva Partida")
    //public void ResetCheckpoint()
    //{
    //    PlayerPrefs.DeleteAll();
    //}
}
