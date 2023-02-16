using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MoveComposite { get; set; }
    public Vector2 MouseDelta { get; set; }
    public bool IsCallMenuPressed { get; set; }

    Controls controls;

    private void OnEnable()
    {
        if (controls != null)
            return;

        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        MouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveComposite = context.ReadValue<Vector2>();
    }

    public void OnCallMenu(InputAction.CallbackContext context)
    {
        IsCallMenuPressed = context.performed;
    }

    public void OnQuit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
}