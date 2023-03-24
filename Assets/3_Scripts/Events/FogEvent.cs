using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FogEvent : Event
{
    [SerializeField] GameObject vista;
    [SerializeField] AK.Wwise.Event activateEvent;
    private Material material;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        vista.SetActive(false);
    }
    public override void Activate()
    {
        material.DOFade(0, 5);
        vista.SetActive(true);
        activateEvent.Post(gameObject);
    }

    public override void Deactivate()
    {
        //material.DOFade(.95f, 5);
    }
}
