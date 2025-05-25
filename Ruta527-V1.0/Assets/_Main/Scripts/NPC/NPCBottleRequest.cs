using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class NPCBottleDeliver : MonoBehaviour
{
    [SerializeField] private GameObject deliverPanel;
    [SerializeField] private Button deliverButton;
    [SerializeField] private int requiredPoints = 5;
    [SerializeField] private GameObject pathToUnlock;
    [SerializeField] private TMP_Text warningText; // Texto para mostrar advertencias

    private bool isPlayerInRange = false;
    private bool hasDelivered = false;

    private void Start()
    {
        deliverPanel.SetActive(false);

        if (warningText != null)
        {
            warningText.gameObject.SetActive(false); // Oculta el texto al inicio
            warningText.color = Color.red; // Establece el color en rojo
        }

        deliverButton.onClick.AddListener(DeliverBottles);
    }

    private void DeliverBottles()
    {
        if (GameManager.Instance.TotalPoints >= requiredPoints)
        {
            GameManager.Instance.RemovePoints(requiredPoints);
            pathToUnlock.SetActive(true);
            hasDelivered = true;
            deliverPanel.SetActive(false);
        }
        else
        {
            if (warningText != null)
                StartCoroutine(ShowWarning("¡No tienes suficientes botellas!"));
        }
    }

    private IEnumerator ShowWarning(string message)
    {
        warningText.text = message;
        warningText.gameObject.SetActive(true);    // Muestra el texto

        yield return new WaitForSeconds(2f);       // Espera 2 segundos

        warningText.gameObject.SetActive(false);   // Oculta el texto
        deliverPanel.SetActive(false);             // Cierra el panel
    }

    private void OnMouseDown()
    {
        if (isPlayerInRange && !hasDelivered)
        {
            deliverPanel.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;

            if (deliverPanel != null)
            {
                deliverPanel.SetActive(false);
            }

            if (warningText != null)
            {
                warningText.gameObject.SetActive(false); // Oculta el mensaje si aún está visible
            }
        }
    }
}
