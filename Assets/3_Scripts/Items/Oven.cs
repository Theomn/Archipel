using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Transformer
{
    private readonly float heatPerBlow = 50;
    private readonly float maxHeat = 120;
    private readonly float heatThreshold = 100;
    private readonly float heatLossPerSecond = 7;
    private float heat;

    private void Update()
    {
        heat -= Time.deltaTime * heatLossPerSecond;
        heat = Mathf.Clamp(heat, 0, maxHeat);
    }

    public void Blow()
    {
        heat += heatPerBlow;
        if (heat > heatThreshold) Forge();
    }

    private void Forge()
    {
        Activate();
    }
}
