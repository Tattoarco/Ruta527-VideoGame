using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinalNPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] messages;
    private int index = 0;

    public float typingSpeed = 0.03f;
    public float messageDelay = 1.5f;

    public GameObject deliverButton;
    public GameObject winPanel;
    public GameObject gameOverPanel;

    private bool dialogueActive = false;

    void OnMouseDown()
    {
        if (!dialogueActive)
        {
            dialogueActive = true;
            dialoguePanel.SetActive(true);
            StartCoroutine(TypeMessage());
        }
    }

    IEnumerator TypeMessage()
    {
        dialogueText.text = "";

        foreach (char letter in messages[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(messageDelay);

        index++;
        if (index < messages.Length)
        {
            StartCoroutine(TypeMessage());
        }
        else
        {
            dialogueText.text = "¿Quieres entregar las botellas?";
            deliverButton.SetActive(true);
        }
    }

    public void DeliverBottles()
    {
        Debug.Log("Entregando botellas...");

        dialoguePanel.SetActive(false);

        if (GameManager.Instance.TotalPoints >= 50)
        {
            Debug.Log("Ganaste el juego.");
            winPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Perdiste el juego.");
            gameOverPanel.SetActive(true);
        }

        Invoke("RestartGame", 5f);
    }


    private void RestartGame()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.ResetGame();
    }
}
