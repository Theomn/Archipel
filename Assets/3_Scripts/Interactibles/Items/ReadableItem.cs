using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableItem : Item
{
    [SerializeField] private string textKey;
    [SerializeField] private TextType textType;

    [SerializeField] private string thoughtKey;
    [SerializeField] private List<string> removeThoughtKeys;

    [SerializeField] AK.Wwise.Event readEvent;
    [SerializeField] AK.Wwise.Event closeEvent;

    protected override void Awake()
    {
        base.Awake();
        isUseable = true;
    }

    public override void Use()
    {
        base.Use();
        readEvent.Post(gameObject);
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
        closeEvent.Post(gameObject);
        HUDController.instance.CloseText(textType);
    }
}
