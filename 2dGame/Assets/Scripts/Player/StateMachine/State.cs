using UnityEngine;
using UnityEngine.InputSystem;
public abstract class State
{
    protected static Animator _playerAnimator;
    protected static StateController _controller;
    protected static InputActionMap _inputMap;
    public void Initialize()
    {
        _playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        _controller = GameObject.Find("Player").GetComponent<StateController>();
        _inputMap = GameObject.Find("InputHandler").GetComponent<PlayerInput>().actions.FindActionMap("Player");
    }

    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();
    public abstract void OnExit();
}