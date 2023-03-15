using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour, Grabbable
{
    [SerializeField] private Transform target;
    public Item heldItem{get; private set;}

    public bool isBlocked {get; private set;}


    public bool isHoldingItem
    {
        get { return heldItem != null; }
        private set {}
    }

    public virtual Vector3 Place(Item item)
    {
        heldItem = item;
        Utils.SetHighSprite(item, target.position.y - transform.position.y);
        item.GetComponent<Collider>().enabled = false;
        return target.position;
    }

    public virtual Item Grab()
    {
        if (isBlocked || heldItem == null)
        {
            return null;
        }
        Utils.ResetHighSprite(heldItem);
        heldItem.GetComponent<Collider>().enabled = true;
        var item = heldItem;
        heldItem = null;
        return item;
    }

    public void SetBlocked(bool blocked)
    {
        isBlocked = blocked;
    }

    public void DestroyItem()
    {
        Destroy(heldItem.gameObject);
        heldItem = null;
    }

    public void SwapItem(Item item)
    {
        Destroy(heldItem.gameObject);
        item.transform.position = Place(item);
    }
}
