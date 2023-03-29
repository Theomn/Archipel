using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Allows taking control of buttons instead of the player
public class ControlToggle
{
    public static bool isActive { get; private set; }
    private static bool activationFlag;
    private static string[] unpauseButtons;
    private static Action closeCallback;
    private Localization loc;

    public static void Update()
    {
        if (activationFlag)
        {
            // prevent unpausing the frame it opens
            isActive = true;
            activationFlag = false;
            return;
        }

        if (isActive)
        {
            foreach (string button in unpauseButtons)
            {
                if (Input.GetButtonDown(button))
                {
                    isActive = false;
                    PlayerController.instance.Pause(false);
                    PlayerItem.instance.Pause(false);
                    if (closeCallback != null) closeCallback();
                }
            }
        }
    }

    public static void TakeControl(Action callback, params string[] unpauseButtons)
    {
        activationFlag = true;
        PlayerController.instance.Pause(true);
        PlayerItem.instance.Pause(true);
        closeCallback = callback;
        ControlToggle.unpauseButtons = unpauseButtons;
        foreach(string button in unpauseButtons)
        {
            var loc = GameController.instance.localization;
            if(button == Button.grab) HUDController.instance.grab.Show(true, loc.GetText("action_return"));
            if(button == Button.use) HUDController.instance.use.Show(true, loc.GetText("action_return"));
            if(button == Button.jump) HUDController.instance.jump.Show(true, loc.GetText("action_return"));
        }
    }
}
