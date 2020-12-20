using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, Player_Input.IFreeroamActions
{
    private Player_Input _playerInput;
    private void Awake()
    {
        _playerInput = new Player_Input();
        _playerInput.Freeroam.SetCallbacks(this);
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        _playerInput.Freeroam.Enable();
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        _playerInput.Freeroam.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 delta = context.ReadValue<Vector2>();

    }
}
