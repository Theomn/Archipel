using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleItem : Item
{
    [SerializeField] private string modifier;
    public override void Use()
    {
        base.Use();
        PlayerModifiers.instance.AddModifier(modifier);
        PlayerItem.instance.RemoveItem();
        Destroy(gameObject);
    }
}
