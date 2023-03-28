using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableStation : MonoBehaviour, Useable
{
    [SerializeField] private string textKey;
    [SerializeField] private TextType textType;
    [SerializeField] private string thoughtKey;
    [SerializeField] private List<string> removeThoughtKeys;
    public void Use()
    {
        HUDController.instance.DisplayText(textType, textKey);
        ControlToggle.TakeControl(Close, Button.use, Button.grab, Button.jump);

        if (thoughtKey != "")
        {
            ThoughtScreen.instance.AddThought(thoughtKey);
        }
        foreach(var key in removeThoughtKeys)
        {
            ThoughtScreen.instance.RemoveThought(key);
        }
    }

    public void Close()
    {
        HUDController.instance.CloseText(textType);
    }
}
