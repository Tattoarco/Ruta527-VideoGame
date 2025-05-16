using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI points;
    public GameObject[] health;
  
    void Update()
    {
        points.text = GameManager.Instance.TotalPoints.ToString();
    }

    public void UpdatePoints(int puntosTotales)
    {
        points.text = puntosTotales.ToString();
    }

    public void DisableHealth(int indice)
    {
        if (indice >= 0 && indice < health.Length)
        {
            health[indice].SetActive(false);
        }
    }


    public void ActiveHealth(int indice)
    {
        health[indice].SetActive(true);
    }
}