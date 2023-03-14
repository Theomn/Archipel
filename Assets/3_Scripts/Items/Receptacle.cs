using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour, Grabbable
{
    [SerializeField] private Transform target;
    public Item heldItem{get; private set;}


    public bool isHoldingItem
    {
        get { return heldItem != null; }
        private set {}
    }

    public Vector3 Place(Item item)
    {
        heldItem = item;
        Utils.LiftSprite(item, target.position.y - transform.position.y);
        item.GetComponent<Collider>().enabled = false;
        return target.position;
    }

    public Item Grab()
    {
        if (heldItem == null)
        {
            return null;
        }
        Utils.ResetSpriteLift(heldItem);
        heldItem.GetComponent<Collider>().enabled = true;
        var item = heldItem;
        heldItem = null;
        return item;
    }

    public void SwapItem(Item item)
    {
        Destroy(heldItem.gameObject);
        item.transform.position = Place(item);
    }
}
