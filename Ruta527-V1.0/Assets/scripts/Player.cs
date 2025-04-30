using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController1 : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float movement;

    private Rigidbody2D rb;

    public float health;
    [SerializeField] private float maxHealth = 100;


    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask mudLayer;
    [SerializeField] public ParticleSystem hit_ps;

    public bool canJump;

    [SerializeField] private Animator playerAnimator;

    public float hitTime;
    public float hitForceX;
    public float hitForceY;
    public bool hitFromRight;

    public TextMeshProUGUI healthText;

    public float particleCount;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        hit_ps = GetComponentInChildren<ParticleSystem>();

        health = maxHealth;
        healthText.text = $"Health {health}/{maxHealth}";
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        // rb.AddForce(new Vector2(movement * speed, 0), ForceMode2D.Force);

        if (movement < 0) //izquierda
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }
        else if (movement > 0)//derecha
        {
            transform.localScale = new Vector3(2, 2, 2);
        }

        if (Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer) || Physics2D.OverlapCircle(groundCheck.position, groundRadius, mudLayer))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        movement = Input.GetAxisRaw("Horizontal");

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }



        playerAnimator.SetFloat("Movement", movement);

        if (hitTime <= 0)
        {
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        }
        else
        {
            if (hitFromRight)
            {
                rb.velocity = new Vector2(-hitForceX, hitForceY);
            }
            else if (!hitFromRight)
            {
                rb.velocity = new Vector2(hitForceX, hitForceY);
            }
            hitTime -= Time.deltaTime;
        }

    }

    public void Takedamage(float _damage, Sprite damageSprite, int BurstCount)
    {
        health -= _damage;


        health = Mathf.Clamp(health, 0, 100);

        healthText.text = $"Health: {health}/{maxHealth}";

        //ParticleSystem.Burst burst = hit_ps.emission.GetBurst(0);
        //burst.count = particleCount;
        hit_ps.textureSheetAnimation.AddSprite(damageSprite);
        ParticleSystem.Burst NewBurst = new ParticleSystem.Burst();
        NewBurst.count = BurstCount;
        hit_ps.emission.SetBurst(0, NewBurst);
        hit_ps.Play();
    }

    public void AddHealth(float _health)
    {
        health += _health;

        health = Mathf.Clamp(health, 0, 100);

        healthText.text = $"Health: {health}/{maxHealth}";

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            speed = 6f;
            jumpForce = 6f;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Mud"))
        {
            speed = 3f;
            jumpForce = 3f;
        }

    }
}