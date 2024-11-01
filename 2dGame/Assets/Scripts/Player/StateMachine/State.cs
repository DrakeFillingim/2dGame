using UnityEngine;
using UnityEngine.InputSystem;
public abstract class State
{
    protected static GameObject _player;
    protected static StateController _controller;
    protected static Rigidbody2D _rb;
    protected static PlayerStats _stats;
    protected static Animator _playerAnimator;
    protected static InputActionMap _inputMap;

    /// <summary>
    /// Grabs a reference to necessary components of the given player, call once per frame on start or enable
    /// </summary>
    /// <param name="player"></param>
    public void Initialize(GameObject player)
    {
        _player = player;
        _controller = player.GetComponent<StateController>();
        _rb = player.GetComponent<Rigidbody2D>();
        _stats = player.GetComponent<PlayerStats>();
        _playerAnimator = player.GetComponent<Animator>();
        _inputMap = GameObject.Find("InputHandler").GetComponent<PlayerInput>().actions.FindActionMap("Player");
    }

    /// <summary>
    /// For logic the state runs every time it becomes the active state
    /// </summary>
    public abstract void OnStart();
    /// <summary>
    /// For logic the state runs every frame
    /// </summary>
    public abstract void OnUpdate();
    /// <summary>
    /// For logic the state runs every physics frame
    /// </summary>
    public abstract void OnFixedUpdate();
    /// <summary>
    /// For logic the state runs every time it stops being the active state
    /// </summary>
    public abstract void OnExit();

    /// <summary>
    /// Connects the state to all input events
    /// </summary>
    public void ConnectEvents()
    {
        _inputMap["Move"].performed += OnMove;
        _inputMap["Jump"].performed += OnJump;
        _inputMap["Dash"].performed += OnDash;
        _inputMap["Crouch"].performed += OnCrouch;
        _inputMap["Run"].performed += OnRun;
        _inputMap["Attack"].performed += OnAttack;
        _inputMap["ChargeAttack"].started += OnChargeAttackStarted;
    }

    /// <summary>
    /// Disconnects the state from all input events to prevent duplicate events
    /// </summary>
    public void DisconnectEvents()
    {
        _inputMap["Move"].performed -= OnMove;
        _inputMap["Jump"].performed -= OnJump;
        _inputMap["Dash"].performed -= OnDash;
        _inputMap["Crouch"].performed -= OnCrouch;
        _inputMap["Run"].performed -= OnRun;
        _inputMap["Attack"].performed -= OnAttack;
        _inputMap["ChargeAttack"].started -= OnChargeAttackStarted;
    }

    /*
     * Following methods left empty and intended to be overridden by states,
     * Can be left empty to have no behavior for the given input.
     */

    protected virtual void OnMove(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnJump(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnDash(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnCrouch(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnRun(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnAttack(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnChargeAttackStarted(InputAction.CallbackContext context)
    {

    }
}