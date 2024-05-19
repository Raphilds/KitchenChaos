using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);   
            }
            else
            {
                // Player does not have a kitchen object
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                // Player has a kitchen object
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Add ingredient to plate
                    if (plateKitchenObject != null)
                    {
                        bool ingredientAdded = plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO());
                        
                        if (ingredientAdded)
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                }
                else
                {
                    // Player is not carrying a plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // Counter is holding a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                // Player does not have a kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
