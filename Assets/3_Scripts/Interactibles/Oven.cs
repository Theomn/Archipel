using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Transformer
{
    [SerializeField] private SpriteRenderer hotSprite;

    [SerializeField] AK.Wwise.Event blowEvent;
    [SerializeField] AK.Wwise.Event forgeEvent;
    private readonly float heatPerBlow = 50;
    private readonly float maxHeat = 120;
    private readonly float heatThreshold = 100;
    private readonly float heatLossPerSecond = 7;
    private float heat;

    private void Update()
    {
        heat -= Time.deltaTime * heatLossPerSecond;
        heat = Mathf.Clamp(heat, 0, maxHeat);
        hotSprite.color = new Color(hotSprite.color.r, hotSprite.color.g, hotSprite.color.b, heat / 100f);
    }

    public void Blow()
    {
        heat += heatPerBlow;
        blowEvent.Post(gameObject);
        if (heat > heatThreshold) Forge();
    }

    private void Forge()
    {
        forgeEvent.Post(gameObject);
        Transform();
    }
}
