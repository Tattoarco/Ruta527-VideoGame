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
            else if (dialogueText.text == dialogueLines[lineIndex]) // Si la l�nea de di�logo actual se ha mostrado completamente
            {
                NextDialougeLine();
            }
            else
            {
                StopAllCoroutines(); // Detiene la escritura actual
                dialogueText.text = dialogueLines[lineIndex]; // Muestra la l�nea completa
            }

        }
    }

    // Inicia el di�logo
    private void StartDialouge()
    {
        didDialogueStart = true;// Inicia el di�logo
        dialoguePanel.SetActive(true);// Activa el panel de di�logo
        dialogueMark.SetActive(false); // Desactiva el icono de di�logo
        lineIndex = 0; // Reinicia el �ndice de l�nea
        StartCoroutine(ShowLine()); // Muestra la primera l�nea
    }

    private void NextDialougeLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length) // Si a�n quedan l�neas
        {
            StartCoroutine(ShowLine()); // Muestra la siguiente l�nea
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false); // Cierra el panel
            dialogueMark.SetActive(true);  // Muestra el �cono de di�logo
            Time.timeScale = 1f; // Reanuda el tiempo del juego
        }
    }


    // Muestra la siguiente l�nea de di�logo
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime );// Espera un tiempo antes de mostrar el siguiente car�cter
        }
       
    }

    //Detecta si ingresas al rango del di�logo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            isPlayerInRange = true; 
            dialogueMark.SetActive(true); // Activa el icono de di�logo

        }
    }

    //Detecta si se sale del rango del di�logo
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            isPlayerInRange = false;
            dialogueMark.SetActive(false); // Desactiva el icono de di�logo

        }
    }
}
