using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorPuzzleManager : MonoBehaviour
{
    [Header("Botones de colores")]
    public Button redButton;
    public Button blueButton;
    public Button yellowButton;
    public Button greenButton;
    public Button orangeButton;
    public Button pinkButton;

    [Header("Colores y Secuencia")]
    [SerializeField] private List<string> colorSequence; // Secuencia objetivo
    private List<string> playerInput = new List<string>();
    private string[] availableColors = { "Red", "Blue", "Yellow", "Green", "Orange", "Pink" };

    [Header("Objetos")]
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;
    public GameObject puzzleUI; // El panel con los botones
    public TMP_Text attemptsText;

    [Header("Configuración")]
    public int sequenceLength = 4;
    public float delayBetweenFlashes = 0.6f;

    private int attemptCount = 0;
    private bool puzzleActive = false;

    void Start()
    {
        // Desactivar elementos
        puzzleUI.SetActive(false);
        objectToActivate.SetActive(false);

        // Asignar eventos a los botones
        redButton.onClick.AddListener(() => OnColorClicked("Red"));
        blueButton.onClick.AddListener(() => OnColorClicked("Blue"));
        yellowButton.onClick.AddListener(() => OnColorClicked("Yellow"));
        greenButton.onClick.AddListener(() => OnColorClicked("Green"));
        orangeButton.onClick.AddListener(() => OnColorClicked("Orange"));
        pinkButton.onClick.AddListener(() => OnColorClicked("Pink"));

        UpdateAttemptsText();
    }

    public void ActivatePuzzle()
    {
        puzzleUI.SetActive(true);
        puzzleActive = true;
        GenerateRandomSequence();
        StartCoroutine(ShowSequence());
    }

    public void DeactivateObject()
    {
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }
    }


    void GenerateRandomSequence()
    {
        colorSequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            string randomColor = availableColors[Random.Range(0, availableColors.Length)];
            colorSequence.Add(randomColor);
        }
    }

    IEnumerator ShowSequence()
    {
        SetButtonsInteractable(false);

        foreach (string color in colorSequence)
        {
            Button btn = GetButtonByColor(color);
            Color original = btn.image.color;
            btn.image.color = Color.white;
            yield return new WaitForSeconds(0.3f);
            btn.image.color = original;
            yield return new WaitForSeconds(delayBetweenFlashes);
        }

        SetButtonsInteractable(true);
    }

    void OnColorClicked(string color)
    {
        if (!puzzleActive) return;

        playerInput.Add(color);

        if (playerInput.Count == colorSequence.Count)
        {
            SetButtonsInteractable(false);
            CheckSequence();
        }
    }

    void CheckSequence()
    {
        attemptCount++;
        UpdateAttemptsText();

        for (int i = 0; i < colorSequence.Count; i++)
        {
            if (playerInput[i] != colorSequence[i])
            {
                Debug.Log("❌ Secuencia incorrecta");
                playerInput.Clear();
                StartCoroutine(ShowSequence());
                return;
            }
        }

        Debug.Log("✅ ¡Secuencia correcta!");
        objectToActivate.SetActive(true);      // Activa el objeto deseado
        DeactivateObject();                    // 🔸 Ahora también desactiva el objeto indicado
        puzzleUI.SetActive(false);             // Oculta el puzzle
        puzzleActive = false;
    }


    void UpdateAttemptsText()
    {
        if (attemptsText != null)
            attemptsText.text = "Intentos: " + attemptCount;
    }

    Button GetButtonByColor(string color)
    {
        return color switch
        {
            "Red" => redButton,
            "Blue" => blueButton,
            "Yellow" => yellowButton,
            "Green" => greenButton,
            "Orange" => orangeButton,
            "Pink" => pinkButton,
            _ => null,
        };
    }

    void SetButtonsInteractable(bool value)
    {
        redButton.interactable = value;
        blueButton.interactable = value;
        yellowButton.interactable = value;
        greenButton.interactable = value;
        orangeButton.interactable = value;
        pinkButton.interactable = value;
    }


}
