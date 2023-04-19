using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugKeys : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ThoughtScreen.instance.DebugPlayNotification();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            DiaryScreen.instance.revealText(5);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerController.instance.ToggleSpeedCheat();
        }
    }
}
