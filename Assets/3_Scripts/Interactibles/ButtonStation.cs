using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStation : UseableStation
{
    public override void Use()
    {
        base.Use();
        GetComponentInChildren<Animator>().SetTrigger("Press");
    }
}
