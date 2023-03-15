using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour
{
    [SerializeField] protected Receptacle receptacle;
    [SerializeField] protected TransformerRecipeBook recipeBook;

    public virtual void Activate()
    {
        if (!receptacle.isHoldingItem)
        {
            return;
        }
        if (recipeBook.Contains(receptacle.heldItem.identifier))
        {
            Recipe recipe = recipeBook.Find(receptacle.heldItem.identifier);
            var outputItem = Instantiate(recipe.outputObject).GetComponent<Item>();
            receptacle.SwapItem(outputItem);
        }
        else if (recipeBook.destroyIfNoRecipe)
        {
            receptacle.DestroyItem();
        }
    }
}
