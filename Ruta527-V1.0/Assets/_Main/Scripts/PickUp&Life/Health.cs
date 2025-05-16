using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.RecoverHealth(); // Llama al método para sumar vida
            Destroy(gameObject); // Destruye el objeto de salud
        }
    }
}
