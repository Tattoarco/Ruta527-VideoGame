using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector3 CurrentAssignment;

    private void Start()
    {
        CurrentAssignment = pointB.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentAssignment, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, CurrentAssignment) < 0.1f)
        {
            // Cambia de dirección
            CurrentAssignment = CurrentAssignment == pointA.position ? pointB.position : pointA.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LoseHealth(); // Llama al método para restar vida
        }
    }
}
