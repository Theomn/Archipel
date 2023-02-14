using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    [SerializeField] private string itemIdentifier;
    [SerializeField] private IEvent activatedEvent;
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
        if (item.identifier.Equals(itemIdentifier))
        {
            activatedEvent.Activate();
        }
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
        if (item.identifier.Equals(itemIdentifier))
        {
            activatedEvent.Deactivate();
        }
    }
}
