using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FogEvent : Event
{
    private Material material;

    private void Awake() {
        material = GetComponent<MeshRenderer>().material;
    }
    public override void Activate()
    {
        material.DOFade(0, 5);
    }

    public override void Deactivate()
    {
        //material.DOFade(.95f, 5);
    }
}
