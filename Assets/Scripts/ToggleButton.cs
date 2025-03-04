using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    public void ToggleScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
