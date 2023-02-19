using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class PlayerItem : SingletonMonoBehaviour<PlayerItem>
{
    [SerializeField] public Transform hands;
    private bool isHoldingItem = false;
    private Item heldItem;
    private PlayerController controller;


    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        controller = PlayerController.instance;
    }

    void Update()
    {
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
            return;
        }
        heldItem.Drop();
        heldItem.transform.parent = null;
        heldItem.transform.DOKill();
        heldItem.transform.DOLocalMove(hit.point, 0.2f);
        isHoldingItem = false;
        heldItem = null;
    }

    private void TakeItem(Item item)
    {
        item.Take();
        item.transform.parent = transform;
        item.transform.DOKill();
        item.transform.DOLocalMove(Vector3.zero, 0.2f);
        isHoldingItem = true;
        heldItem = item;
    }

    private bool CanDropItem()
    {
        return Physics.CheckSphere(transform.position + PlayerController.instance.forward, 0.5f);
    }

    private float distanceToPlayer(Transform t)
    {
        return (t.position - transform.position).sqrMagnitude;
    }
}
