using UnityEngine;
using TMPro;
using System.Collections;

public class InstructionController : MonoBehaviour
{
    public GameObject mensajePanel;       // GameObject con Image + TextMeshProUGUI
    public TextMeshProUGUI mensajeUI;     // Solo el texto, dentro del mensajePanel
    public string[] mensajes;

    private int indiceMensaje = 0;
    private Coroutine mensajeCoroutine;

    private void Start()
    {
        if (mensajePanel != null)
        {
            mensajePanel.SetActive(false); // Oculta todo el cartel con texto
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (mensajeCoroutine == null && mensajePanel != null)
            {
                mensajeCoroutine = StartCoroutine(MostrarMensaje());
            }
        }
    }

    private IEnumerator MostrarMensaje()
    {
        if (mensajes.Length > 0)
        {
            mensajeUI.text = mensajes[indiceMensaje];
            mensajePanel.SetActive(true);       // Muestra cartel + texto

            yield return new WaitForSeconds(2f);

            mensajePanel.SetActive(false);      // Oculta cartel + texto

            mensajeCoroutine = null;
            indiceMensaje = (indiceMensaje + 1) % mensajes.Length;
        }
    }
}
