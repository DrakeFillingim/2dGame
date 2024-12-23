using UnityEngine;

/// <summary>
/// Base class for all nodes to inherit from, must override <c>Evaluate</c>
/// </summary>
public abstract class Node
{
    public enum NodeStates
    {
        Failure,
        Success,
        Running
    }

    public event System.Action NodeSuccess;
    public event System.Action NodeFailure;

    public Weight WeightComponent { get; set; }

    protected NodeStates _nodeState;

    public Node(Weight weightComponent)
    {
        WeightComponent = weightComponent;
        if (WeightComponent != null)
        {
            NodeSuccess += WeightComponent.OnSuccess;
        }
    }

    protected void RaiseNodeSuccess()
    {
        NodeSuccess?.Invoke();
    }

    protected void RaiseNodeFailure()
    {
        NodeFailure?.Invoke();
    }

    public abstract NodeStates Evaluate();

    public class Weight
    {
        private float _value = 0;
        /// <summary>
        /// Cost of successfully running the node.
        /// </summary>
        private float _actionWeight = 0;
        /// <summary>
        /// The "highest" priority the node can be. Between 0 and 1.
        /// </summary>
        private float _baseWeight = 0;
        /// <summary>
        /// The time the node takes to go from full weight (1) to reset (_baseWeight).
        /// </summary>
        private float _decrementTime = 1;
        /// <summary>
        /// Should the weight be treated as linear or not. False values will make the weight
        /// instantly jump from 1 to _baseWeight after _decrementTime has passed.
        /// </summary>
        private bool _lerpWeight = true;
        /// <summary>
        /// Last time the nodes weight has been updated.
        /// </summary>
        private float _updateTime = 0;

        /// <summary>
        /// Used to decide the order in which weighted composite nodes are run.
        /// Weight of 0 is prioritized the highest, 1 the lowest.
        /// Get method updates the node weight to the correct value only when the node is checked.
        /// Set method the node weight and marks the node as dirty for the parent to re-sort.
        /// </summary>
        public float Value
        {
            get
            {
                if (_lerpWeight)
                {
                    Value = _value - (((1 - _baseWeight) / _decrementTime) * (Time.time - _updateTime));
                    _updateTime = Time.time;
                }
                else
                {
                    Value = Mathf.Ceil(_value - ((Time.time - _updateTime) / _decrementTime)) * _value;
                }
                return _value;
            }

            set
            {
                float toSet = Mathf.Clamp(value, _baseWeight, 1);
                if (toSet != _value)
                {
                    Dirty = true;
                }
                _value = toSet;
            }
        }

        /// <summary>
        /// Used to check if the composite node holding this node needs to sort its children.
        /// </summary>
        public bool Dirty { get; set; } = true;

        public Weight(float actionWeight = 0, float baseWeight = 0, float decrementTime = 1, bool lerpWeight = true)
        {
            _baseWeight = baseWeight;
            _actionWeight = actionWeight;
            _decrementTime = decrementTime;
            _lerpWeight = lerpWeight;
        }

        /// <summary>
        /// Subtracts the correct weight every time the node is successfully run
        /// and sets the last time the node weight was updated to the current time.
        /// </summary>
        public void OnSuccess()
        {
            Value += _actionWeight;
            _updateTime = Time.time;
        }
    }
}