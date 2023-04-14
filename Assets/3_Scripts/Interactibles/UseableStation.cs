using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableStation : MonoBehaviour, Useable
{
    [SerializeField] private Event activatedEvent;
    [SerializeField] private Transform highlightPosition;
    public virtual void Use()
    {
        activatedEvent?.Activate();
    }

    public virtual Vector3 GetHighlightPosition()
    {
        return highlightPosition ? highlightPosition.position : transform.position;
    }

    public string GetUseTextKey()
    {
        return "action_use";
    }

    public bool IsUseable()
    {
        return true;
    }
}
