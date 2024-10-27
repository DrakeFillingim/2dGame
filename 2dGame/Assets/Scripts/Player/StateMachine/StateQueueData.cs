public class StateQueueData
{
    public State TransitionState { get; private set; }
    public float BufferDuration { get; private set; }

    public StateQueueData(State transitionState, float bufferDuration)
    {
        TransitionState = transitionState;
        BufferDuration = bufferDuration;
    }
}