using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierItem : Item
{
    public override void Take(float heightFromGround)
    {
        base.Take(heightFromGround);
        PlayerModifiers.instance.AddModifier(identifier);
    }

    public override void Drop()
    {
        base.Drop();
        PlayerModifiers.instance.RemoveModifier(identifier);
    }
}
