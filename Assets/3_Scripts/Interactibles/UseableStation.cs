using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableStation : MonoBehaviour, Useable
{
    [SerializeField] private Event activatedEvent;
    public void Use()
    {
        activatedEvent.Activate();
    }
}
