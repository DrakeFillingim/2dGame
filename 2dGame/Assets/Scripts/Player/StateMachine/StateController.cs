using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StateController : MonoBehaviour
{
    private const int StateQueueLimit = 8;
    private static readonly Dictionary<Type, Type[]> StateMap = new()
    {
        { typeof(IdleState), new Type[] {typeof(WalkState)} },
        { typeof(WalkState), new Type[] {typeof(IdleState)} },
        { typeof(JumpState), new Type[] {} }
    };

    private List<StateQueueData> _stateQueue = new(StateQueueLimit);
    
    private OverwritableStack<State> _previousStates = new();
    private State _currentState;

    private void Start()
    {
        GameObject.Find("InputHandler").GetComponent<PlayerInput>().actions.FindActionMap("Player")["Reset"].performed += _ => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _currentState = new IdleState();
        _currentState.Initialize();
        _currentState.OnStart();
    }

    private void FixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }

    private void Update()
    {
        _currentState.OnUpdate();
        ReadStateQueue();
    }

    /// <summary>
    /// Pushes a state the end the of the player's state queue
    /// </summary>
    /// <param name="toAdd"></param>
    public void AddStateToQueue(StateQueueData toAdd)
    {
        if (_stateQueue.Count < StateQueueLimit)
        {
            _stateQueue.Add(toAdd);
            if (toAdd.BufferDuration > 0)
            {
                Timer.CreateTimer(gameObject, () => _stateQueue.Remove(toAdd), toAdd.BufferDuration, true, false);
            }
        }
    }

    /// <summary>
    /// Reads the current state queue. Breaks after a successful state change, 
    /// removes all non-buffered states that can't transition
    /// </summary>
    private void ReadStateQueue()
    {
        List<StateQueueData> _reversedQueue = _stateQueue.AsEnumerable().Reverse().ToList();
        for (int i = _stateQueue.Count - 1; i >= 0; i--)
        {
            StateQueueData data = _reversedQueue[i];
            if (StateMap[_currentState.GetType()].Contains(data.TransitionState.GetType()))
            {
                print("transitioned from " + _currentState.GetType() + " to " + data.TransitionState.GetType());
                SetState(data.TransitionState);
                _stateQueue.RemoveAt(_stateQueue.Count - 1 - i);
                break;
            }
            else if (data.BufferDuration == 0)
            {
                _stateQueue.RemoveAt(_stateQueue.Count - 1 - i);
            }
        }
    }

    /// <summary>
    /// Sets the players state and calls <c>OnStart</c>. Adds the previous state to <c>_previousStates</c> after calling <c>OnExit</c>
    /// </summary>
    /// <param name="toSet"></param>
    private void SetState(State toSet)
    {
        _currentState.OnExit();
        _previousStates.Push(_currentState);
        _currentState = toSet;
        _currentState.OnStart();
    }
}