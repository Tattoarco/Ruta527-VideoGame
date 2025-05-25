using UnityEngine;

public class Agua : MonoBehaviour
{
    [SerializeField] private float tiempoMaximoEnAgua = 2f;
    private float tiempoEnAgua = 0f;
    private bool jugadorDentro = false;
    private PlayerMovement jugador;
    private AudioSource audioSource;

    [SerializeField] private AudioClip deathSound;

    [Header("Sonido al entrar al agua")]
    [SerializeField] private AudioClip splashSound; 

    private void Update()
    {
        if (jugadorDentro && jugador != null)
        {
            tiempoEnAgua += Time.deltaTime;

            if (tiempoEnAgua >= tiempoMaximoEnAgua)
            {
                GameManager.Instance.LoseHealth();

                if (audioSource != null && deathSound != null)
                    audioSource.PlayOneShot(deathSound);

                tiempoEnAgua = 0f;
                jugador.RespawnPlayerExternamente(); // Llamamos un método desde PlayerMovement
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugador = other.GetComponent<PlayerMovement>();
            if (jugador != null)
            {
                audioSource = jugador.GetComponent<AudioSource>();
                tiempoEnAgua = 0f;
                jugadorDentro = true;

             
                if (audioSource != null && splashSound != null)
                    audioSource.PlayOneShot(splashSound);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
            tiempoEnAgua = 0f;
        }
    }
}
