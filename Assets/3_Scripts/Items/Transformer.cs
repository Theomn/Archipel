using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : Event
{
    [SerializeField] private Receptacle receptacle;
    [SerializeField] private TransformerRecipeBook recipeBook;

    public override void Activate()
    {
        if (recipeBook.Contains(receptacle.heldItem.identifier))
        {
            Recipe recipe = recipeBook.Find(receptacle.heldItem.identifier);
            var outputItem = Instantiate(recipe.outputObject).GetComponent<Item>();
            receptacle.SwapItem(outputItem);
        }
    }

    public override void Deactivate()
    {
        return;
    }
}
