using UnityEngine;

public class CastleFrontAutoHide : MonoBehaviour
{
    public GameObject keypadPanel; // Asigna el KeypadPanel desde el Inspector

    private bool hasChecked = false;

    void Update()
    {
        if (!hasChecked && keypadPanel != null && !keypadPanel.activeInHierarchy)
        {
            // Se desactiva este GameObject (CastleFront)
            gameObject.SetActive(false);
            hasChecked = true; // Solo lo hace una vez
        }
    }
}
