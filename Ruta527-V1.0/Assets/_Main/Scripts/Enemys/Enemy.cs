using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public int health = 3;
    private Vector3 CurrentAssignment;
    private bool hasDamagedPlayer = false;
    public GameObject deathParticlesPrefab;


    private void Start()
    {
        CurrentAssignment = pointB.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentAssignment, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, CurrentAssignment) < 0.1f)
        {
            CurrentAssignment = CurrentAssignment == pointA.position ? pointB.position : pointA.position;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LoseHealth();
            hasDamagedPlayer = true;  // Marca que este enemigo le quitó vida al jugador
        }
    }


    public void TakeDamage()
    {
        Debug.Log("Enemigo recibió daño");

        if (hasDamagedPlayer)
        {
            GameManager.Instance.RecoverHealth();
        }

        // Instanciar partículas
        if (deathParticlesPrefab != null)
        {
            Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

}
