using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transformer : Event
{
    [SerializeField] protected Receptacle receptacle;
    [SerializeField] protected TransformerRecipeBook recipeBook;

    public virtual bool Transform()
    {
        if (!receptacle.isHoldingItem)
        {
            return false;
        }
        if (recipeBook.Contains(receptacle.heldItem.identifier))
        {
            Recipe recipe = recipeBook.Find(receptacle.heldItem.identifier);
            var outputItem = Instantiate(recipe.outputObject).GetComponent<Item>();
            receptacle.SwapItem(outputItem);
            return true;
        }

        return false;
    }
}
