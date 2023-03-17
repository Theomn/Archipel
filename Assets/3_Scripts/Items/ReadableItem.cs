using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableItem : Item
{
    [SerializeField] private string textKey;
    [SerializeField] private TextType textType;
    public override void Use()
    {
        base.Use();
        HUDController.instance.DisplayText(textType, textKey);
        ControlToggle.TakeControl(Close, Button.use, Button.grab, Button.jump);
    }

    public void Close()
    {
        HUDController.instance.CloseText(textType);
    }
}
