using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableStation : MonoBehaviour, Useable
{
    [SerializeField] private Event activatedEvent;
    public virtual void Use()
    {
        activatedEvent?.Activate();
    }

    public virtual Vector3 GetHighlightPosition()
    {
        return transform.position;
    }

    public string GetUseTextKey()
    {
        return "action_use";
    }
}
