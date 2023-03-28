using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableReceptacle : Receptacle, Useable
{
    [SerializeField] private Event ev;
    public void Use()
    {
        ev.Activate();
    }
}
