using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherReceptacle : Receptacle
{
    [SerializeField] private string stoneIdentifier;
    [SerializeField] private Crusher crusher;
    public override Vector3 Place(Item item)
    {
        var output = base.Place(item);
        if (item.identifier.Equals(stoneIdentifier))
        {
            crusher.Rise();
        }
        return output;
    }

    public override Item Grab()
    {
        var item = base.Grab();
        if (item == null)
        {
            return null;
        }
        
        if (crusher.isUp)
        {
            crusher.Fall();
        }
        return item;
    }

}
