using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class PlayerItem : SingletonMonoBehaviour<PlayerItem>
{
    [SerializeField] private LayerMask dropLayerMask;
    [SerializeField] private Transform hands;
    [SerializeField] private float handsLenght;
    [SerializeField] public Transform mouth;

    public Vector3 initialHandsPosition { get; private set; }
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
        ThoughtScreen.instance.AddThought("test_bateau");
    }

    void Update()
    {
        hands.localPosition = initialHandsPosition + SnapHandPosition();
        if (isHoldingItem)
        {
            if (Input.GetButtonDown("Use"))
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
            if (Input.GetButtonDown("Grab") || Input.GetButtonDown("Use"))
            {
                var colliders = Physics.OverlapSphere(transform.position + controller.forward * 0.9f, 1f, 1 << Layer.item);
                if (colliders.Length > 0)
                {
                    var closest = colliders.OrderBy(c => distanceToPlayer(c.transform)).ElementAt(0);
                    Item item;
                    Provider provider;
                    if ((item = closest.GetComponent<Item>()) != null)
                    {
                        TakeItem(closest.GetComponent<Item>());

                    }
                    else if ((provider = closest.GetComponent<Provider>()) != null)
                    {
                        TakeItem(provider.Provide());
                    }
                }
            }
        }
    }

    private void DropItem()
    {
        if (!Physics.Raycast(transform.position + controller.forward * 0.5f + Vector3.up, Vector3.down, out var hit, 1.5f))
        {
            // No surface to drop item
            return;
        }
        heldItem.Drop();
        heldItem.transform.parent = null;
        heldItem.transform.DOKill();
        heldItem.transform.DOLocalMove(hit.point, 0.25f).SetEase(Ease.InCirc);
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
        if (!item)
        {
            return;
        }
        item.Take(hands.localPosition.y);
        item.transform.parent = hands;
        item.transform.DOKill();
        item.transform.DOLocalMove(Vector3.zero, 0.25f).SetEase(Ease.InOutCirc);
        isHoldingItem = true;
        heldItem = item;
    }

    private bool CanDropItem()
    {
        // Can drop item if there is nothing in front of player
        var canDropItem = !Physics.CheckSphere(transform.position + PlayerController.instance.forward * 0.5f, 0.25f, dropLayerMask);
        return canDropItem;
    }

    private float distanceToPlayer(Transform t)
    {
        //return Vector3.Distance(t.position, transform.position);
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
        return snapPosition * handsLenght + Vector3.back * 0.01f; // little offset backwards to make sure it appears in front of player when sideways
    }
}
