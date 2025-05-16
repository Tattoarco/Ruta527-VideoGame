using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float delayBeforeFall = 2f;
    [SerializeField] private float restoreTime = 5f;

    private float timer;
    private bool playerOnPlatform = false;
    private bool hasFallen = false;

    private TilemapCollider2D tilemapCollider;
    private Tilemap tilemap;

    void Start()
    {
        tilemapCollider = GetComponent<TilemapCollider2D>();
        tilemap = GetComponent<Tilemap>();
    }

    void Update()
    {
        if (playerOnPlatform && !hasFallen)
        {
            timer += Time.deltaTime;
            if (timer >= delayBeforeFall)
            {
                Fall();
            }
        }
    }
    
    void Fall()
    {
        hasFallen = true;
        tilemapCollider.enabled = false;
        SetTilemapTransparency(0.3f);
        Invoke("RestorePlatform", restoreTime);
    }

    void RestorePlatform()
    {
        hasFallen = false;
        tilemapCollider.enabled = true;
        SetTilemapTransparency(1f);
        timer = 0;
    }

    // Cambia la transparencia del Tilemap
    void SetTilemapTransparency(float alpha)
    {
        Color c = tilemap.color;
        c.a = alpha;
        tilemap.color = c;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Verifica si el objeto que colisiona tiene la etiqueta "Player"
        if (collision.collider.CompareTag("Player"))
        {
            playerOnPlatform = true;
            timer = 0;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //Verifica si el objeto que sale de la colisión tiene la etiqueta "Player"
        if (collision.collider.CompareTag("Player"))
        {
            playerOnPlatform = false;
            timer = 0;
        }
    }
}
