using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Recipe
{
    public string inputIdentifier;
    public GameObject outputObject;
}

[CreateAssetMenu(fileName = "New Recipe Book", menuName = "Scriptable Objects/Transformer Recipe Book", order = 1)]

public class TransformerRecipeBook : ScriptableObject
{
    public List<Recipe> recipes;

    public Recipe Find(string identifier)
    {
        return recipes.Find(r => r.inputIdentifier.Equals(identifier));
    }

    public bool Contains (string identifier)
    {
        return recipes.Exists(r => r.inputIdentifier.Equals(identifier));
    }
}
