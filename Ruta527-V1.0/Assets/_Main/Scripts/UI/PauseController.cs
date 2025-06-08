using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenu; // Panel de pausa

    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false); // Asegúrate de que esté oculto al iniciar
        Time.timeScale = 1;         // Juego corriendo normalmente
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void Continuar()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAInicio()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Inicio"); // Asegúrate que se llame exactamente así la escena
    }
}
