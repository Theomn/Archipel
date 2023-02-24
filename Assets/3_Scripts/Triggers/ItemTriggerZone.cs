using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTriggerZone : MonoBehaviour
{
    [SerializeField] private string itemIdentifier;
    [SerializeField] private Event activatedEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.item)
        {
            return;
        }
        Item item = other.GetComponent<Item>();
        if (!item)
        {
            Debug.LogWarning("Object on item layer does not contain Item component.");
            return;
        }
        if (!item.identifier.Equals(itemIdentifier))
        {
            return;
        }

        activatedEvent.Activate();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != Layer.item)
        {
            return;
        }
        Item item = other.GetComponent<Item>();
        if (!item)
        {
            Debug.LogWarning("Object on item layer does not contain Item component.");
            return;
        }
        if (!item.identifier.Equals(itemIdentifier))
        {
            return;
        }

        activatedEvent.Deactivate();
    }
}
