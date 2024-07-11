using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputCotroller : MonoBehaviour
{
    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }
    public Vector3 getMoveVector(float speed)
    {
        Vector2 move = inputActions.Player.Move.ReadValue<Vector2>();
        Vector3 newVelocity = Vector3.zero;
        if (move.x != 0 || move.y != 0)
        {
            newVelocity = new Vector3(move.x, 0, move.y);
        }
        return newVelocity * speed * Time.deltaTime;
    }

    public bool isJump()
    {
        return inputActions.Player.Jump.ReadValue<float>() > 0;
    }
}
