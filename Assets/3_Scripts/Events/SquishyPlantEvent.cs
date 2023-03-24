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

    private void Awake()
    {
        coll = GetComponent<Collider>();
        initialScale = visual.localScale.y;
    }

    public override void Activate()
    {
        coll.enabled = false;
        visual.DOKill();
        visual.DOScaleY(initialScale * 0.3f, 0.8f);
        activateEvent.Post(gameObject);
    }

    public override void Deactivate()
    {
        coll.enabled = true;
        visual.DOKill();
        visual.DOScaleY(initialScale, 0.8f);
        deactivateEvent.Post(gameObject);
    }
}
