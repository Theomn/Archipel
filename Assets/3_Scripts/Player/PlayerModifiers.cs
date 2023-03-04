using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerModifiers : SingletonMonoBehaviour<PlayerModifiers>
{
    // key, timer
    private Dictionary<string, float> modifiers;

    protected override void Awake()
    {
        base.Awake();
        modifiers = new Dictionary<string, float>();
    }

    private void Update()
    {
        foreach (string key in modifiers.Keys.ToList())
        {
            // Timer below -20 are considered infinite
            if (modifiers[key] < -20f)
            {
                continue;
            }

            modifiers[key] -= Time.deltaTime;
            if (modifiers[key] <= 0)
            {
                modifiers.Remove(key);
            }
        }
    }

    public void AddModifier(string modifier)
    {
        if (modifiers.ContainsKey(modifier))
        {
            modifiers[modifier] = -100f;
        }
        else
        {
            modifiers.Add(modifier, -100f);
        }
    }

    public void AddModifier(string modifier, float time)
    {
        if (modifiers.ContainsKey(modifier))
        {
            modifiers[modifier] = time;
        }
        else
        {
        modifiers.Add(modifier, time);

        }
    }

    public void RemoveModifier(string modifier)
    {
        modifiers.Remove(modifier);
    }

    public bool ContainsModifier(string modifier)
    {
        return modifiers.ContainsKey(modifier);
    }
}
