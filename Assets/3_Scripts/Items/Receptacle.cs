using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour, Grabbable
{
    public Item heldItem{get; private set;}
    [SerializeField] private Transform target;
    [SerializeField] private AK.Wwise.Event blabla;

    public bool isHoldingItem
    {
        get { return heldItem != null; }
        private set {}
    }

    public Vector3 Place(Item item)
    {
        blabla.Post(gameObject);
        heldItem = item;
        item.LiftSprite(target.position.y - transform.position.y);
        item.GetComponent<Collider>().enabled = false;
        return target.position;

    }

    public Item Grab()
    {
        if (heldItem == null)
        {
            return null;
        }
        heldItem.ResetSprite();
        heldItem.GetComponent<Collider>().enabled = true;
        var item = heldItem;
        heldItem = null;
        return item;
    }
}
