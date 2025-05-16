using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayerPrefs.SetFloat("CheckpointX", other.transform.position.x);
            PlayerPrefs.SetFloat("CheckpointY", other.transform.position.y);
            PlayerPrefs.Save(); // Guarda los datos
        }
    }

}
