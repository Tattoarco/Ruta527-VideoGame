using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorZone : MonoBehaviour
{
    [SerializeField] private Collider2D confinerCollider;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (confinerCollider != null)
        {
            confinerCollider.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (confinerCollider != null)
        {
            confinerCollider.enabled = true;
        }
    }
}
