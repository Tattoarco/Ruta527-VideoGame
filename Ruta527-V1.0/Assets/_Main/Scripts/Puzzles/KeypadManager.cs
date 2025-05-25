using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class KeypadManager : MonoBehaviour
{
    public GameObject keypadPanel;
    public GameObject closedDoor;
    public GameObject castleFront;

    public TextMeshProUGUI displayText;
    public List<Button> buttons;
    public Color pressedColor = Color.gray;
    public string correctCode = "2810";

    public AudioClip keyPressSound;
    private AudioSource audioSource;

    private string currentInput = "";

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();


        keypadPanel.SetActive(false);
        displayText.text = "";

        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => PressKey(btn));
        }
    }

    public void ShowKeypad()
    {
        currentInput = "";
        displayText.text = "";
        ResetButtonColors();
        keypadPanel.SetActive(true);
    }

    public void PressKey(Button buttonPressed)
    {
        string number = buttonPressed.GetComponentInChildren<TMP_Text>().text;
        currentInput += number;
        displayText.text = currentInput;

        // Reproducir sonido
        if (keyPressSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(keyPressSound);
        }

        // Cambiar color
        ColorBlock colors = buttonPressed.colors;
        colors.normalColor = pressedColor;
        buttonPressed.colors = colors;

        if (currentInput.Length >= correctCode.Length)
        {
            CheckCode();
        }
    }

    private void CheckCode()
    {
        if (currentInput == correctCode)
        {
            StartCoroutine(ShowCastleSequence());
        }
        else
        {
            currentInput = "";
            displayText.text = "";
            ResetButtonColors();
        }
    }

    private IEnumerator ShowCastleSequence()
    {
        keypadPanel.SetActive(false);
        closedDoor.SetActive(false);
        castleFront.SetActive(true);
        yield return new WaitForSeconds(1f);
        castleFront.SetActive(false);
    }

    private void ResetButtonColors()
    {
        foreach (Button btn in buttons)
        {
            var colors = btn.colors;
            colors.normalColor = Color.white;
            btn.colors = colors;
        }
    }
}
