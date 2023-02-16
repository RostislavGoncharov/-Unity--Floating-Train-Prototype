using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10.0f;
    [SerializeField] float lookSensitivity = 10.0f;

    [SerializeField] private Canvas scenesMenu;

    [SerializeField] private Transform lookTarget;

    private PlayerInput playerInput;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction callMenuAction;
    private InputAction quitAction;

    private InputActionMap uiActions;

    private CharacterController charController;

    private float cameraRotation = 0;
    private bool isInMenu = false;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        callMenuAction = playerInput.actions["CallMenu"];
        quitAction = playerInput.actions["Quit"];

        uiActions = playerInput.actions.FindActionMap("UI");

        charController = GetComponent<CharacterController>();

        Camera.main.transform.LookAt(lookTarget);
    }

    void Update()
    {
        // Reading the movement input as Vector2, converting it to Vector3, then applying the movement.
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 move = transform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y));

        charController.Move(move * movementSpeed * Time.deltaTime);

        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        cameraRotation -= lookInput.y * lookSensitivity * Time.deltaTime;
        cameraRotation = Mathf.Clamp(cameraRotation, -50f, 50f);

        transform.Rotate(Vector3.up * lookInput.x * lookSensitivity * Time.deltaTime);
        Camera.main.transform.localRotation = Quaternion.Euler(cameraRotation, 0, 0);
        
        if (callMenuAction.IsPressed() || uiActions.FindAction("CloseMenu").IsPressed())
        {
            ToggleScenesMenu();
        }

        if (quitAction.IsPressed())
        {
            Application.Quit();
        }
    }

    public void ToggleScenesMenu()
    {
        scenesMenu.gameObject.SetActive(!scenesMenu.isActiveAndEnabled);
        isInMenu = !isInMenu;

        if(isInMenu)
        {
            playerInput.SwitchCurrentActionMap("UI");
        }

        else
        {
            playerInput.SwitchCurrentActionMap("Player");
        }
    }
}
