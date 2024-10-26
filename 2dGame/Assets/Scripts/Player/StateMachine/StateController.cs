using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateController : MonoBehaviour
{
    private Queue<StateQueueData> _stateQueue = new();

    private void Start()
    {
        GameObject.Find("InputHandler").GetComponent<PlayerInput>().actions.FindActionMap("Player")["Move"].performed += _ => Debug.Log(_.ReadValue<float>());
    }

    private void Update()
    {
        
    }

    public void AddStateToQueue(IState toAdd)
    {

    }
}

public class StateQueueData
{

}

public interface IState
{

}