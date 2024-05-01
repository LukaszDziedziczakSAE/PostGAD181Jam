using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    Controls controls;

    [field: SerializeField, Header("Debug")] public Vector2 Movement { get; private set; }
    [field: SerializeField] public Vector2 Look { get; private set; }
    [field: SerializeField] public bool Running { get; private set; }
    [field: SerializeField] public bool Sneaking { get; private set; }
    [field: SerializeField] public bool Aiming { get; private set; }
    [field: SerializeField] public bool Attacking { get; private set; }

    public event Action OnAttackPress;
    public event Action OnAimingPress;
    public event Action OnSneakingPress;
    public event Action OnRunningPress;
    public event Action OnMenuPress;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnRunning(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Running = true;
            OnRunningPress?.Invoke();
        }
        else if (context.canceled) Running = false;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>().normalized;
    }

    public void OnSneak(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Sneaking = true;
            OnSneakingPress?.Invoke();
        }
        else if (context.canceled) Sneaking = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attacking = true;
            OnAttackPress?.Invoke();
        }
        else if (context.canceled) Attacking = false;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Aiming = true;
            OnAimingPress?.Invoke();
        }
        else if (context.canceled) Aiming = false;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) OnMenuPress?.Invoke();
    }
}
