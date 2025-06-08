using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;

    public int keysCollected = 0;
    public int totalKeys = 5; // Ahora espera 5 llaves
    public GameObject tileToUnlock; // Objeto bloqueado que se desactivará al recoger todas las llaves

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddKey()
    {
        keysCollected++;
        Debug.Log("Llaves recogidas: " + keysCollected); // Verifica en consola

        if (keysCollected >= totalKeys && tileToUnlock != null)
        {
            tileToUnlock.SetActive(false); // Elimina el bloqueo
            Debug.Log("✅ Todas las llaves recolectadas. ¡Camino desbloqueado!");
        }
    }
}
