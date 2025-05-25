using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector3 CurrentAssignment;
    private bool hasDamagedPlayer = false;

    private void Start()
    {
        CurrentAssignment = pointB.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentAssignment, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, CurrentAssignment) < 0.1f)
        {
            // Cambia el destino
            CurrentAssignment = CurrentAssignment == pointA.position ? pointB.position : pointA.position;

            // Invierte visualmente el sprite en el eje X
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
            hasDamagedPlayer = true;
        }
    }

    public void TakeDamage()
    {
        if (hasDamagedPlayer)
        {
            GameManager.Instance.RecoverHealth(); // Recupera vida si antes le hizo daño
        }

        Destroy(gameObject); // Destruye el enemigo
    }
}
