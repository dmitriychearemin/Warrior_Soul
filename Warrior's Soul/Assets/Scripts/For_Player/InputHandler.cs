using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset _action;
    [SerializeField] private string actionMap = "Player";

    private InputAction moveAction;
    private InputAction runAction;
    private InputAction attackAction;
    private InputAction mousePosAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 MousePosInput { get; private set; }
    public bool RunTriggered { get; private set; }
    public bool AttackTriggered { get; private set; }
    public static InputHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        moveAction = _action.FindActionMap(actionMap).FindAction("Move");
        mousePosAction = _action.FindActionMap(actionMap).FindAction("MousePosition");
        runAction = _action.FindActionMap(actionMap).FindAction("Run");
        attackAction = _action.FindActionMap(actionMap).FindAction("Attack");
        RegisterInputActions();
    }

    private void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        mousePosAction.performed += context => MousePosInput = context.ReadValue<Vector2>();
        mousePosAction.canceled += context => MousePosInput = Vector2.zero;

        attackAction.performed += context => AttackTriggered = true;
        attackAction.canceled += context => AttackTriggered = false;

        runAction.performed += context => RunTriggered = true;
        runAction.canceled += context => RunTriggered = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        mousePosAction.Enable();
        runAction.Enable();
        attackAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        mousePosAction.Disable();
        runAction.Disable();
        attackAction.Disable();
    }
}
