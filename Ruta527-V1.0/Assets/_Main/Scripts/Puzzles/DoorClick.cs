using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class DoorClick : MonoBehaviour
{
    public KeypadManager keypadManager;

    private void OnMouseDown()
    {
        keypadManager.ShowKeypad();
    }
}
