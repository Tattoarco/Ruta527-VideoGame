using UnityEngine;

public class DeletePrefsOnStart : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs eliminados al iniciar el juego.");
    }
}
