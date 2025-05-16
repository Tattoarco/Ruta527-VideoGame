using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
	public HUD hud;

    public int TotalPoints {get; private set;}

	private int health = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Cuidado! Mas de un GameManager en escena.");
        }
    }

    public void AddPoints(int pointsToAdd)
    {
        TotalPoints += pointsToAdd;
		hud.UpdatePoints(TotalPoints);
    }

    public void LoseHealth()
    {
        if (health > 0)
        {
            hud.DisableHealth(health - 1); // activa/desactiva el corazón correcto
            health -= 1;
        }

        if (health == 0)
        {
            SceneManager.LoadScene(0);
        }
    }


    public bool RecoverHealth() {
		if (health == 3)
		{
			return false;
		}

		hud.ActiveHealth(health);
        health += 1;
		return true;
	}
}