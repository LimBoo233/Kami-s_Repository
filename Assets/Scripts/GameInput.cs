using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    private PlayerInputActions playerInputActions;
    
    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Move.performed += MoveOnperformed;
    }

    private void MoveOnperformed(InputAction.CallbackContext obj) {
        throw new NotImplementedException();
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        // inputVector = inputVector.normalized;
        return inputVector;
    }
    
    
}