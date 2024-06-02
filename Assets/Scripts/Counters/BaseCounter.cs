using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;
    
    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }
    
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    
    public virtual void Interact(Player player)
    {
        Debug.Log("Interacting with base counter");
    }
    
    public virtual void InteractAlternate(Player player)
    {
        //Debug.Log("Interacting alternate with base counter");
    }
    
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObjectParameter)
    {
        this.kitchenObject = kitchenObjectParameter;

        if (kitchenObject is not null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);            
        }
    }
    
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    
    public bool HasKitchenObject()
    {
        return kitchenObject is not null;
    }
}
