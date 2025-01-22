using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    //The name of the Input Action Map we want to access for the controller
    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    //The names of the actions we want to use in the game
    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string attack = "Fire";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string jump = "Jump";

    //Variables to set up the handler
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction attackAction;
    private InputAction sprintAction;
    private InputAction jumpAction;
    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    public bool attackTriggered { get; private set; }
    public float sprintValue { get; private set; }
    public bool jumpTriggered { get; private set; }

    //Looks for the actions with the given strings and sets them to an InputAction variable
    private void Awake()
    {
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        attackAction = playerControls.FindActionMap(actionMapName).FindAction(attack);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        RegisterInputActions();
    }

    //Whenever any action is performed, it saves its context into a variable to be used in other scripts
    private void RegisterInputActions()
    {
        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;

        lookAction.performed += context => lookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => lookInput = Vector2.zero;

        attackAction.performed += context => attackTriggered = true;
        attackAction.canceled += context => attackTriggered = false;

        sprintAction.performed += context => sprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => sprintValue = 0f;

        jumpAction.performed += context => jumpTriggered = true;
        jumpAction.canceled += context => jumpTriggered = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        attackAction.Enable();
        sprintAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        attackAction.Disable();
        sprintAction.Disable();
        jumpAction.Disable();
    }

}
