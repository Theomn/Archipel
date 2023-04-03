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

    private void Awake()
    {
        coll = GetComponent<Collider>();
        initialScale = visual.localScale.y;
    }

    public override void Activate()
    {
        if (isActive) return;

        isActive = true;
        coll.enabled = false;
        visual.DOKill();
        visual.DOScaleY(initialScale * 0.3f, 0.8f);
        activateEvent.Post(gameObject);
    }

    public override void Deactivate()
    {
        if (!isActive) return;

        isActive = false;
        coll.enabled = true;
        visual.DOKill();
        visual.DOScaleY(initialScale, 0.8f);
        deactivateEvent.Post(gameObject);
    }
}
