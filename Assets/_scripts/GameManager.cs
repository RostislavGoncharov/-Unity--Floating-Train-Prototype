using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private PlayerInput playerInput;
    private InputAction quitAction;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        playerInput = GetComponent<PlayerInput>();
        quitAction = playerInput.actions["Quit"];
    }

    void Update()
    {
        if (quitAction.IsPressed())
        {
            Application.Quit();
        }
    }
}
