using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HarvestItem : Item
{
    private PlayerController controller;
    protected override void Start()
    {
        base.Start();
        controller = PlayerController.instance;
    }

    public override void Use()
    {
        base.Use();
        var colliders = Physics.OverlapSphere(transform.position + controller.forward * 0.9f, 1f, 1 << Layer.interactible);
        var c = Array.Find(colliders, x => x.GetComponent<Harvestable>() != null);
        if (!c) return;

        c.GetComponent<Harvestable>().Harvest(this);
    }
}
