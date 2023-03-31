using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventReceptacle : Receptacle
{
    [SerializeField] private string requiredIdentifier;
    [SerializeField] private Event activatedEvent;

    [SerializeField] private List<string> removeThoughtKeys;

    public override Vector3 Place(Item item)
    {
        if (item.identifier.Equals(requiredIdentifier))
        {
            activatedEvent.Activate();
            foreach (var key in removeThoughtKeys)
            {
                ThoughtScreen.instance.RemoveThought(key);
            }
        }
        return base.Place(item);
    }

    public override Item Grab()
    {
        var item = base.Grab();
        if (item && item.identifier.Equals(requiredIdentifier))
        {
            activatedEvent.Deactivate();
        }
        return item;
    }
}
