using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the players state. Externally change the players state via <c>AddStateToQueue</c>
/// </summary>
public class StateController : MonoBehaviour
{
    private const int StateQueueLimit = 8;

    /// <summary>
    /// Dictionary that controls which states can transition to other states
    /// </summary>
    private static readonly Dictionary<Type, Type[]> StateMap = new()
    {
        { typeof(FallState), new Type[] { typeof(IdleState), typeof(JumpState), typeof(DashState), typeof(JumpAttackState) } },
        { typeof(IdleState), new Type[] { typeof(FallState), typeof(JumpState), typeof(DashState), typeof(WalkState), typeof(CrouchState), typeof(RunState), typeof(LightAttackState), typeof(ChargeAttackState) } },
        { typeof(JumpState), new Type[] { typeof(FallState), typeof(JumpState), typeof(DashState), typeof(JumpAttackState) } },
        { typeof(DashState), new Type[] { typeof(FallState), typeof(IdleState), typeof(WalkState), typeof(SlideState) } },
        { typeof(WalkState), new Type[] { typeof(FallState), typeof(IdleState), typeof(JumpState), typeof(DashState), typeof(CrouchState), typeof(RunState), typeof(LightAttackState), typeof(ChargeAttackState) } },
        { typeof(CrouchState), new Type[] { typeof(FallState), typeof(IdleState), typeof(DashState), typeof(WalkState), typeof(RunState), typeof(LightAttackState), typeof(ChargeAttackState) } },
        { typeof(RunState), new Type[] { typeof(FallState), typeof(IdleState), typeof(JumpState), typeof(WalkState), typeof(SlideState), typeof(MovementAttackState), typeof(ChargeAttackState) } },
        { typeof(SlideState), new Type[] { typeof(FallState), typeof(JumpState), typeof(CrouchState), typeof(RunState) } },
        { typeof(JumpAttackState), new Type[] { typeof(FallState), typeof(IdleState) } },
        { typeof(LightAttackState), new Type[] { typeof(IdleState) } },
        { typeof(ChargeAttackState), new Type[] { typeof(FallState), typeof(IdleState), typeof(LightAttackState) } },
        { typeof(MovementAttackState), new Type[] { typeof(IdleState) } },
    };

    public OverwritableStack<Type> previousStates = new();
    private List<StateQueueData> _stateQueue = new(StateQueueLimit);
    private State _currentState;

    public event Action<Type> PlayerStateChange;

    private void Start()
    {
        GameObject.Find("InputHandler").GetComponent<PlayerInput>().actions.FindActionMap("Player")["Reset"].performed += _ => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameObject.Find("InputHandler").GetComponent<PlayerInput>().actions.FindActionMap("Player")["Pause"].performed += _ => Debug.Break();
        _currentState = new IdleState();
        _currentState.Initialize(gameObject);
        _currentState.ConnectEvents();
        _currentState.OnStart();
    }

    private void Update()
    {
        _currentState.OnUpdate();
    }

    private void FixedUpdate()
    {
        _currentState.OnFixedUpdate();
        ReadStateQueue();
        //print("in " + _currentState.GetType());
    }

    private void OnDisable()
    {
        _currentState.DisconnectEvents();
        _currentState.OnExit();
    }


    /// <summary>
    /// Pushes a state the end the of the player's state queue. 
    /// The state is ignored if the player already has 8 items in queue.
    /// </summary>
    /// <param name="toAdd"></param>
    public void AddStateToQueue(StateQueueData toAdd)
    {
        if (_stateQueue.Count < StateQueueLimit)
        {
            _stateQueue.Add(toAdd);
            if (toAdd.BufferDuration > 0)
            {
                Timer.CreateTimer(gameObject, (toAdd.TransitionState.GetType() + " Buffer Timer"),() => _stateQueue.Remove(toAdd), toAdd.BufferDuration, true, false);
            }
        }
    }

    /// <summary>
    /// Reads the current state queue. Breaks after a successful state change, 
    /// removes all non-buffered states that can't transition.
    /// </summary>
    private void ReadStateQueue()
    {
        List<StateQueueData> _reversedQueue = _stateQueue.AsEnumerable().Reverse().ToList();
        for (int i = _stateQueue.Count - 1; i >= 0; i--)
        {
            StateQueueData data = _reversedQueue[i];
            if (StateMap[_currentState.GetType()].Contains(data.TransitionState.GetType()))
            {
                //states added in OnStart immediately if next to lines swapped
                _stateQueue.RemoveAt(_stateQueue.Count - 1 - i);
                SetState(data.TransitionState);
                ClearDestructableStates();
                break;
            }
            else if (data.BufferDuration == 0)
            {
                _stateQueue.RemoveAt(_stateQueue.Count - 1 - i);
            }
        }
    }

    /// <summary>
    /// Forces an update of the current state
    /// </summary>
    public void ForceStateUpdate()
    {
        ReadStateQueue();
    }

    /// <summary>
    /// Sets the players state and calls <c>OnStart</c>. Adds the previous state to <c>_previousStates</c> after calling <c>OnExit</c>
    /// </summary>
    /// <param name="toSet"></param>
    private void SetState(State toSet)
    {
        print("transition from " + _currentState + " to " + toSet);
        PlayerStateChange?.Invoke(toSet.GetType());
        _currentState.DisconnectEvents();
        _currentState.OnExit();
        previousStates.Push(_currentState.GetType());
        _currentState = toSet;
        _currentState.ConnectEvents();
        _currentState.OnStart();
    }

    private void ClearDestructableStates()
    {
        for (int i = _stateQueue.Count - 1; i >= 0; i--)
        {
            if (_stateQueue[i].Destructable)
            {
                _stateQueue.RemoveAt(i);
            }
        }
    }
}