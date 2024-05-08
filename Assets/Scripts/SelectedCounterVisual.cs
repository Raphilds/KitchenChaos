using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;        
    }

    private void PlayerOnSelectedCounterChanged(object sender, Player.SelectedCounterChangedEventArgs e)
    {
        HandleSelectedCounterChanged(e.selectedCounter == baseCounter);
    }

    //Show or hide the visualGameObject
    private void HandleSelectedCounterChanged(bool isSelectedCounter)
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(isSelectedCounter);
        }
    }
}
