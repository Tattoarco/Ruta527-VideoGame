using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatforMovement_1 : MonoBehaviour
{
    public Transform pointA;
    public float speed = 2f;
    public Transform player;
    public float triggerDistance = 0.3f; // Activación muy cercana

    private bool shouldMove = false;

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > triggerDistance)
        {
            shouldMove = true;
        }

        if (shouldMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
        }
    }
}
