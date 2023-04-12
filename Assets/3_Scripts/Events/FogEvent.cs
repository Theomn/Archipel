using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FogEvent : Event
{
    [SerializeField] GameObject vista;

    [SerializeField] GameObject boat;
    [SerializeField] AK.Wwise.Event activateEvent;

    private void Awake()
    {
        if (vista) vista.SetActive(false);
        boat.SetActive(false);
    }
    public override void Activate()
    {
        if (vista) vista.SetActive(true);
        activateEvent.Post(gameObject);
        boat.SetActive(true);
        gameObject.SetActive(false);
    }

    public override void Deactivate()
    {
        //material.DOFade(.95f, 5);
    }
}
