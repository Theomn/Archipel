using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FogEvent : Event
{
    [SerializeField] GameObject vista;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] ParticleSystem particles1, particles2;

    [SerializeField] GameObject boat;
    [SerializeField] AK.Wwise.Event activateEvent;

    private void Awake()
    {
        if (vista) vista.SetActive(false);
        if (boat) boat.SetActive(false);
    }
    public override void Activate()
    {
        if (vista) vista.SetActive(true);
        if (sprite) sprite.DOFade(0, 5f);
        if (particles1) particles1.Stop();
        if (particles2) particles2.Stop();
        activateEvent.Post(gameObject);
        if (boat) boat.SetActive(true);
    }

    public override void Deactivate()
    {

    }
}
