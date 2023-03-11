using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour
{
    private Item heldItem;
    [SerializeField] private Transform target;

    public bool isHoldingItem
    {
        get { return heldItem != null; }
        private set {}
    }

    public Vector3 Place(Item item)
    {
        heldItem = item;
        item.LiftSprite(target.position.y - transform.position.y);
        item.GetComponent<Collider>().enabled = false;
        return target.position;
    }

    public Item Retrieve()
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
