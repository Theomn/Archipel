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
    string GetUseTextKey();
    bool IsUseable();
}

public interface Grabbable : Interactible
{
    Item Grab();

    string GetGrabTextKey();
}
