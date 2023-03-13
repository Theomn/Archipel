using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactible
{

}
public interface Useable : Interactible
{
    void Use();
}

public interface Grabbable : Interactible
{
    Item Grab();
}
