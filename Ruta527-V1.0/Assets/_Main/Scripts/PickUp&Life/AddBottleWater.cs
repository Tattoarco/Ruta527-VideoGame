using UnityEngine;

public class AddBottleWater : MonoBehaviour
{
    public int value = 1;
    public AudioClip pickupSound; // Asigna el sonido desde el Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reproduce el sonido en la posición de la botella
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            GameManager.Instance.AddPoints(value);
            Destroy(gameObject);
        }
    }
}
