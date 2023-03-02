using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModifiers : SingletonMonoBehaviour<PlayerModifiers>
{
    private List<string> modifiers;

    protected override void Awake()
    {
        base.Awake();
        modifiers = new List<string>();
    }

    public void AddModifier(string modifier)
    {
        if (modifiers.Contains(modifier))
        {
            return;
        }
        modifiers.Add(modifier);
    }

    public void RemoveModifier(string modifier)
    {
        modifiers.Remove(modifier);
    }

    public bool ContainsModifier(string modifier)
    {
        return modifiers.Contains(modifier);
    }
}
