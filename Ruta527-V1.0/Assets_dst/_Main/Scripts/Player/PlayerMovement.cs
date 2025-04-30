using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canDoubleJump;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento horizontal con A y D
        float moveX = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
        }

        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        // Verifica si está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Si toca el suelo, puede volver a hacer doble salto
        if (isGrounded)
        {
            canDoubleJump = true;
        }

        // Salto normal con W estando en el suelo
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Doble salto solo si presiona Shift + W y no está en el suelo
        if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.W) && !isGrounded && canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = false;
        }
    }
}
