using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Layer
{
    public static readonly int interactible = LayerMask.NameToLayer("Interactible");
    public static readonly int ground = LayerMask.NameToLayer("Ground");
    public static readonly int player = LayerMask.NameToLayer("Player");
    public static readonly int wall = LayerMask.NameToLayer("Wall");
}