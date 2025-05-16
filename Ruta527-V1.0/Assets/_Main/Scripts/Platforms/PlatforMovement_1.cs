using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatforMovement_1 : MonoBehaviour
{
    [SerializeField] private Transform point; // Punto al que se moverá la plataforma
    [SerializeField] private float speed = 2f;

    private bool shouldMove = false;

    void Update()
    {
        if (shouldMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shouldMove = true;
        }
    }
}
