using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Oven : Transformer
{
    [SerializeField] private SpriteRenderer hotSprite;
    [SerializeField] private ParticleSystem smokeParticle;
    [SerializeField] private ParticleSystem forgeParticle;

    [SerializeField] AK.Wwise.Event startEvent;
    [SerializeField] AK.Wwise.Event forgeEvent;
    /*private readonly float heatPerBlow = 50;
    private readonly float maxHeat = 120;
    private readonly float heatThreshold = 100;
    private readonly float heatLossPerSecond = 7;
    private float heat;*/

    private void Awake()
    {
        hotSprite.color = new Color(hotSprite.color.r, hotSprite.color.g, hotSprite.color.b, 0);
    }
    /*private void Update()
    {
        heat -= Time.deltaTime * heatLossPerSecond;
        heat = Mathf.Clamp(heat, 0, maxHeat);
        hotSprite.color = new Color(hotSprite.color.r, hotSprite.color.g, hotSprite.color.b, heat / 100f);
    }*/

    public void Blow()
    {
        /*heat += heatPerBlow;
        blowEvent.Post(gameObject);
        if (heat > heatThreshold) Forge();*/
    }

    private void Forge()
    {
        forgeEvent.Post(gameObject);
        hotSprite.DOFade(0, 1f);
        smokeParticle.Stop();
        if (Transform())
        {
            //smokeParticle.Clear();
            forgeParticle.Emit(100);
        }
        else forgeParticle.Emit(5);
    }

    public override void Activate()
    {
        hotSprite.DOKill();
        hotSprite.DOFade(1, 3f).OnComplete(Forge);
        smokeParticle.Play();
        startEvent.Post(gameObject);
    }

    public override void Deactivate()
    {
        return;
    }
}
