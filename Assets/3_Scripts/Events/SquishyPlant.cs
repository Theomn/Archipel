using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SquishyPlant : Event
{
    [SerializeField] private Transform visual;
    private Collider coll;
    private float initialScale;

    private void Awake()
    {
        coll = GetComponent<Collider>();
        initialScale = transform.localScale.y;
    }

    public override void Activate()
    {
        coll.enabled = false;
        visual.DOKill();
        visual.DOScaleY(initialScale * 0.3f, 0.8f);
    }

    public override void Deactivate()
    {
        coll.enabled = true;
        visual.DOKill();
        visual.DOScaleY(initialScale, 0.8f);
    }
}
