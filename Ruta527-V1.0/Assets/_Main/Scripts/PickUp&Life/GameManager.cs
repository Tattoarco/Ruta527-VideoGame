using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
	public HUD hud;

    public int TotalPoints {get; private set;}

	private int health = 5;

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

    public void RemovePoints(int pointsToRemove)
    {
        TotalPoints -= pointsToRemove;
        if (TotalPoints < 0)
            TotalPoints = 0;

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
            // Eliminar checkpoint y reiniciar la escena actual
            PlayerPrefs.DeleteKey("CheckpointX");
            PlayerPrefs.DeleteKey("CheckpointY");
            PlayerPrefs.DeleteKey("HasCheckpoint");

            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }



    public bool RecoverHealth() {
		if (health == 5)
		{
			return false;
		}

		hud.ActiveHealth(health);
        health += 1;
		return true;
	}

    public void ReducePoints(int amount)
    {
        TotalPoints -= amount;
        if (TotalPoints < 0)
            TotalPoints = 0;

        hud.UpdatePoints(TotalPoints); // si tienes referencia al HUD
    }

    public void ResetGame()
    {
        TotalPoints = 0;
        health = 5;

        hud.UpdatePoints(TotalPoints);
        for (int i = 0; i < hud.health.Length; i++)
        {
            hud.ActiveHealth(i);
        }
    }

}