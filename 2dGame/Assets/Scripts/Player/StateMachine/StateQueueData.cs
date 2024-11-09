using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateQueueData
{
    public State TransitionState { get; private set; }
    public float BufferDuration { get; private set; }
    public bool Destructable { get; private set; }

    public StateQueueData(State transitionState, float bufferDuration = 0, bool destructable = false)
    {
        TransitionState = transitionState;
        BufferDuration = bufferDuration;
        Destructable = destructable;
    }
}
