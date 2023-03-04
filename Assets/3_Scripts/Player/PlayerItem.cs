using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class PlayerItem : SingletonMonoBehaviour<PlayerItem>
{
    [SerializeField] private Transform hands;
    [SerializeField] private float handsLenght;
    public Vector3 initialHandsPosition {get; private set;}
    private bool isHoldingItem = false;
    private Item heldItem;
    private PlayerController controller;


    protected override void Awake()
    {
        base.Awake();
        initialHandsPosition = hands.localPosition;
    }

    void Start()
    {
        controller = PlayerController.instance;
    }

    void Update()
    {
        hands.localPosition = initialHandsPosition + SnapHandPosition();
        if (isHoldingItem)
        {
            if (Input.GetButton("Use"))
            {
                heldItem.Use();
            }
            else if (Input.GetButtonDown("Grab") && CanDropItem())
            {
                DropItem();
            }
        }

        else if (!isHoldingItem)
        {
            if (Input.GetButtonDown("Grab"))
            {
                var colliders = Physics.OverlapSphere(transform.position + controller.forward, 1f, 1 << Layer.item);
                if (colliders.Length > 0)
                {
                    colliders.OrderBy(c => distanceToPlayer(c.transform));
                    TakeItem(colliders[0].GetComponent<Item>());
                }
            }
        }
    }

    private void DropItem()
    {
        if (!Physics.Raycast(transform.position + controller.forward + Vector3.up, Vector3.down, out var hit, 2.5f))
        {
            // No surface to drop item
            return;
        }
        heldItem.Drop();
        heldItem.transform.parent = null;
        heldItem.transform.DOKill();
        heldItem.transform.DOLocalMove(hit.point, 0.2f);
        isHoldingItem = false;
        heldItem = null;
    }

    public void RemoveItem()
    {
        isHoldingItem = false;
        heldItem = null;
    }

    private void TakeItem(Item item)
    {
        item.Take();
        item.transform.parent = hands;
        item.transform.DOKill();
        item.transform.DOLocalMove(Vector3.zero, 0.2f);
        isHoldingItem = true;
        heldItem = item;
    }

    private bool CanDropItem()
    {
        // Can drop item if there is nothing in front of player
        var canDropItem = !Physics.CheckSphere(transform.position + Vector3.up + PlayerController.instance.forward, 0.45f, -1 ^ Layer.player);
        return canDropItem;
    }

    private float distanceToPlayer(Transform t)
    {
        return (t.position - transform.position).sqrMagnitude;
    }

    private Vector3 SnapHandPosition()
    {
        Vector3 snapPosition;
        if (Mathf.Abs(controller.forward.x) > Mathf.Abs(controller.forward.z))
        {
            snapPosition = new Vector3(Mathf.Sign(controller.forward.x), 0, 0);
        }
        else
        {
            snapPosition = new Vector3(0, 0, Mathf.Sign(controller.forward.z));
        }
        return snapPosition * handsLenght;
    }
}
