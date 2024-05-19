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
            }
            else
            {
                // Player does not have a kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
