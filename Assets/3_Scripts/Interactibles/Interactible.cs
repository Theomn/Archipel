using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactible
{
    Vector3 GetHighlightPosition();
}
public interface Useable : Interactible
{
    void Use();
}

public interface Grabbable : Interactible
{
    Item Grab();
}
