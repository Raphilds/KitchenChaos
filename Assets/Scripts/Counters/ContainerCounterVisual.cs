using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private readonly int OPEN_CLOSE = Animator.StringToHash("OpenClose");
    
    [SerializeField] private ContainerCounter containerCounter;
    
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounterOnPlayerGrabbedObject;
    }

    private void ContainerCounterOnPlayerGrabbedObject(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
