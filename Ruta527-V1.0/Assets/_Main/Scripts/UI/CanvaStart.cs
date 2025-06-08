using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvaStart : MonoBehaviour
{
    public AudioSource sonidoClick;

    public void Jugar()
    {
        sonidoClick.Play();
        SceneManager.LoadScene("Nivel 1");
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("El juego se ha cerrado.");
    }
}
