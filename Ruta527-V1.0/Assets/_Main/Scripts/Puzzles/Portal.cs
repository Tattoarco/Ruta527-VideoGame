using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject portalSalida;
    private GameObject playerGO;
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerGO.transform.position = portalSalida.transform.position - new Vector3(0, 0.7f, 0);
    }
}
