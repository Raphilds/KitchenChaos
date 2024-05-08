using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;        
    }

    private void PlayerOnSelectedCounterChanged(object sender, Player.SelectedCounterChangedEventArgs e)
    {
        HandleSelectedCounterChanged(e.selectedCounter == clearCounter);
    }

    //Show or hide the visualGameObject
    private void HandleSelectedCounterChanged(bool isSelectedCounter)
    {
        visualGameObject.SetActive(isSelectedCounter);
    }
}
