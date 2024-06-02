using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    
    public event EventHandler OnInteractionAction;
    public event EventHandler OnInteractionAlternateAction;
    public event EventHandler OnPauseAction;
    
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        Instance = this;
        
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        
        playerInputActions.Player.Interact.performed += InteractPerformed;     
        playerInputActions.Player.InteractAlternate.performed += InteractAlternatePerformed;
        playerInputActions.Player.Pause.performed += Pause_Performed;
    }
    
    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= InteractPerformed;     
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternatePerformed;
        playerInputActions.Player.Pause.performed -= Pause_Performed;
        
        playerInputActions.Dispose();
    }

    private void Pause_Performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternatePerformed(InputAction.CallbackContext obj)
    {
        OnInteractionAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractPerformed(InputAction.CallbackContext obj)
    {
        OnInteractionAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
