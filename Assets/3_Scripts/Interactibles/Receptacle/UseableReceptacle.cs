using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableReceptacle : Receptacle, Useable
{
    [SerializeField] private Event ev;
    [SerializeField] private string useTextKey;
    public void Use()
    {
        ev.Activate();
    }

    public string GetUseTextKey()
    {
        return useTextKey != "" ? useTextKey : "action_use";
    }
}
