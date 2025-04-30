using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform[] enemyMovementPoints;

    [SerializeField] private Transform actualObjective;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator enemyAnimator;

    public float enemySpeed;
    public float detectionRadius = 0.5f;
    Vector2 movement;
    public Sprite damageSprite;
    public int BurstCount;
    public float enemyHitStrenghtY;
    public float enemyHitStrenghtX;
    public float enemyDamage;
    public float hitTime;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        actualObjective = enemyMovementPoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToObjective = Vector2.Distance(transform.position, actualObjective.position);

        if (distanceToObjective < detectionRadius)
        {
            if (actualObjective == enemyMovementPoints[0])
            {
                actualObjective = enemyMovementPoints[1];
            }
            else if (actualObjective == enemyMovementPoints[1])
            {
                actualObjective = enemyMovementPoints[0];
            }
        }

        Vector2 direction = (actualObjective.position - transform.position).normalized;

        int roundedDirection = Mathf.RoundToInt(direction.x);
        movement = new Vector2(roundedDirection, 0);

        if (roundedDirection < 0)
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }
        else if (roundedDirection > 0)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }

        enemyAnimator.SetFloat("Movement", roundedDirection);

    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * enemySpeed * Time.deltaTime);

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController1 player = collision.gameObject.GetComponent<PlayerController1>();

            player.Takedamage(enemyDamage, damageSprite, BurstCount);
            player.hitTime = hitTime;
            player.hitForceX = enemyHitStrenghtX;
            player.hitForceY = enemyHitStrenghtY / 2;

            if (collision.transform.position.x <= transform.position.x)
            {
                player.hitFromRight = true;
            }
            else if (collision.transform.position.x <= transform.position.x)
            {
                player.hitFromRight = false;
            }

            enemyAnimator.SetTrigger("isAttacking");


        }
    }
}
