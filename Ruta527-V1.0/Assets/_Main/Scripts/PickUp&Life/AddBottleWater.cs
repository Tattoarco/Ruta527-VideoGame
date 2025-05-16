using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBottleWater : MonoBehaviour
{
    public int value = 1; // Valor que se sumar� al puntaje
    public GameManager gameManager; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.AddPoints(value); // Llama al m�todo para sumar puntos
            Destroy(gameObject);
        }
    }   
}
