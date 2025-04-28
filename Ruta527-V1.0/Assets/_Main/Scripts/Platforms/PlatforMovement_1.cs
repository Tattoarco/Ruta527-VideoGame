using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatforMovement_1 : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed;
    public Transform player;
    public float triggerDistance;

    private bool shouldMove = false;
    private Vector3 target;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        // Si el jugador está cerca, activa el movimiento
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < triggerDistance)
        {
            shouldMove = true;
        }

        // Si debe moverse, que lo haga entre A y B
        if (shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                // Cambiar el destino cuando llega a uno de los puntos
                target = target == pointA.position ? pointB.position : pointA.position;
            }
        }
    }
}