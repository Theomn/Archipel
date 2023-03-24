using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableItem : Item
{
    [SerializeField] private string textKey;
    [SerializeField] private TextType textType;

    [SerializeField] AK.Wwise.Event readEvent;
    [SerializeField] AK.Wwise.Event closeEvent;
    public override void Use()
    {
        base.Use();
        readEvent.Post(gameObject);
        HUDController.instance.DisplayText(textType, textKey);
        ControlToggle.TakeControl(Close, Button.use, Button.grab, Button.jump);
    }

    public void Close()
    {
        closeEvent.Post(gameObject);
        HUDController.instance.CloseText(textType);
    }
}
