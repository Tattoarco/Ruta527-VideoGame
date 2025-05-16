using System.Collections;
using UnityEngine;
using TMPro;

public class Dialouge : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;

    private float typingTime = 0.05f; // Velocidad de escritura

    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    void Update()
    {
        if(isPlayerInRange && Input.GetButtonDown("Fire1"))
        {
            if (!didDialogueStart) 
            { 
                StartDialouge();
            }
            else if (dialogueText.text == dialogueLines[lineIndex]) // Si la línea de diálogo actual se ha mostrado completamente
            {
                NextDialougeLine();
            }
            else
            {
                StopAllCoroutines(); // Detiene la escritura actual
                dialogueText.text = dialogueLines[lineIndex]; // Muestra la línea completa
            }

        }
    }

    // Inicia el diálogo
    private void StartDialouge()
    {
        didDialogueStart = true;// Inicia el diálogo
        dialoguePanel.SetActive(true);// Activa el panel de diálogo
        dialogueMark.SetActive(false); // Desactiva el icono de diálogo
        lineIndex = 0; // Reinicia el índice de línea
        StartCoroutine(ShowLine()); // Muestra la primera línea
    }

    private void NextDialougeLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length) // Si aún quedan líneas
        {
            StartCoroutine(ShowLine()); // Muestra la siguiente línea
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false); // Cierra el panel
            dialogueMark.SetActive(true);  // Muestra el ícono de diálogo
            Time.timeScale = 1f; // Reanuda el tiempo del juego
        }
    }


    // Muestra la siguiente línea de diálogo
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime );// Espera un tiempo antes de mostrar el siguiente carácter
        }
       
    }

    //Detecta si ingresas al rango del diálogo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            isPlayerInRange = true; 
            dialogueMark.SetActive(true); // Activa el icono de diálogo

        }
    }

    //Detecta si se sale del rango del diálogo
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            isPlayerInRange = false;
            dialogueMark.SetActive(false); // Desactiva el icono de diálogo

        }
    }
}
