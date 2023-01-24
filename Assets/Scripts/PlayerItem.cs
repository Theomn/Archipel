using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : SingletonMonoBehaviour<PlayerItem>
{
    [SerializeField] private Transform hands;
    private bool isHoldingItem = false;
    private Item heldItem;

    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isHoldingItem)
        {
            if (Input.GetButton("Use"))
            {
                heldItem.Use();
            }
            else if (Input.GetButtonDown("Grab"))
            {
                heldItem.Drop();
                heldItem.transform.parent = null;
                heldItem.transform.localPosition = transform.position + transform.forward;
                isHoldingItem = false;
                heldItem = null;
            }
        }

        else if (!isHoldingItem)
        {
            if (Input.GetButtonDown("Grab"))
            {
                if (Physics.SphereCast(transform.position, 1f, transform.forward, out var hit, 1f, 1 << Layer.item))
                {
                    var item = hit.collider.GetComponent<Item>();
                    item.Take();
                    item.transform.parent = hands;
                    item.transform.localPosition = Vector3.zero;
                    isHoldingItem = true;
                    heldItem = item;
                }
            }
        }
    }
}
