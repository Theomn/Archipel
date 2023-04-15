using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour, Grabbable, Useable
{
    [SerializeField] private Transform target;

    [SerializeField] private Item startItem;
    [SerializeField] private string inspectTextKey;

    public Item heldItem { get; private set; }

    public bool isBlocked { get; private set; }


    public bool isHoldingItem
    {
        get { return heldItem != null; }
        private set { }
    }

    private void Start()
    {
        if (startItem)
        {
            startItem.transform.position = Place(startItem);
        }
    }

    public virtual Vector3 Place(Item item)
    {
        heldItem = item;
        item.transform.parent = transform;
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

    public virtual void Use()
    {
        HUDController.instance.DisplayText(TextType.Popup, inspectTextKey);
        ControlToggle.TakeControl(Close);
        CameraController.instance.ZoomTo(transform, 0.3f);
    }

    private void Close()
    {
        HUDController.instance.CloseText(TextType.Popup);
        CameraController.instance.ResetToPlayer();
    }

    public virtual Vector3 GetHighlightPosition()
    {
        return target.position;
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

    public virtual string GetGrabTextKey()
    {
        return "action_grab";
    }

    public virtual bool IsUseable()
    {
        return inspectTextKey != "";
    }

    public virtual string GetUseTextKey()
    {
        return "action_inspect";
    }
}
