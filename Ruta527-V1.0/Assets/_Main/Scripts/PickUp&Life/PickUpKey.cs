using UnityEngine;
using UnityEngine.UI;

public class PickUpKey : MonoBehaviour
{
    public Image keyIconUI;
    public GameObject solidKeyImage;
    public AudioClip pickupSound;
    public AudioSource audioSource; // ← Asignable desde el Inspector

    private void Start()
    {
        keyIconUI.enabled = false;

        if (solidKeyImage != null)
            solidKeyImage.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickupSound != null && audioSource != null)
                audioSource.PlayOneShot(pickupSound);

            if (solidKeyImage != null)
                StartCoroutine(ShowCollectedKey());

            if (keyIconUI != null)
                keyIconUI.enabled = true;

            KeyManager.instance.AddKey(); // ← Ya sin parámetro

            Destroy(gameObject);
        }
    }

    private System.Collections.IEnumerator ShowCollectedKey()
    {
        solidKeyImage.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        solidKeyImage.SetActive(false);
    }
}
