using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableReceptacle : Receptacle, Useable
{
    [SerializeField] private Event ev;
    [SerializeField] private string useTextKey;
    public override void Use()
    {
        ev.Activate();
    }

    public override string GetUseTextKey()
    {
        return useTextKey != "" ? useTextKey : "action_use";
    }

    public override bool IsUseable()
    {
        return true;
    }
}
