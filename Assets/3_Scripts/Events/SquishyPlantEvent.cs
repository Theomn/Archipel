using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SquishyPlantEvent : Event
{
    [SerializeField] private Transform visual;

    [SerializeField] AK.Wwise.Event activateEvent;
    [SerializeField] AK.Wwise.Event deactivateEvent;
    private Collider coll;
    private float initialScale;

    private bool isActive = false;

    private SpriteRenderer sprite;

    private void Awake()
    {
        coll = GetComponent<Collider>();
        sprite = visual.GetComponent<SpriteRenderer>();
        initialScale = visual.localScale.y;
    }

    public override void Activate()
    {
        if (isActive) return;

        isActive = true;
        coll.isTrigger = true;
        visual.DOKill();
        sprite.DOKill();
        visual.DOScaleY(initialScale * 0.3f, 0.8f);
        sprite.DOColor(Swatches.HexToColor("E39E90"), 1.2f);
        activateEvent.Post(gameObject);
    }

    public override void Deactivate()
    {
        if (!isActive) return;

        isActive = false;
        coll.isTrigger = false;
        visual.DOKill();
        sprite.DOKill();
        visual.DOScaleY(initialScale, 0.8f);
        sprite.DOColor(Color.white, 1.2f);
        deactivateEvent.Post(gameObject);
    }
}
