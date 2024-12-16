using UnityEngine;

/// <summary>
/// For nodes that should be weighted to avoid repeated actions or act as cooldowns.
/// These can be used as normal Nodes which will ignore weight, but normal Nodes cannot
/// be used as though they had weight.
/// </summary>
public abstract class WeightedNode : Node
{
    private float _nodeWeight = 0;

    /// <summary>
    /// Used to decide the order in which weighted composite nodes are run.
    /// Weight of 0 is prioritized the highest, 1 the lowest.
    /// </summary>
    public float NodeWeight
    {
        get
        {
            if (_lerpWeight)
            {
                NodeWeight = _nodeWeight - (((1 - _baseWeight) / _decrementTime) * (Time.time - updateTime));
                updateTime = Time.time;
            }
            else
            {
                NodeWeight = Mathf.Ceil(1 - ((Time.time - updateTime) / _decrementTime));
            }
            return _nodeWeight;
        }

        set
        {
            float toSet = Mathf.Clamp(value, _baseWeight, 1);
            if (toSet != _nodeWeight)
            {
                Dirty = true;
            }
            _nodeWeight = toSet;
        }
    }

    /// <summary>
    /// Used to determine if the composite node holding a reference needs to sort its children.
    /// </summary>
    public bool Dirty { get; private set; } = true;

    private float _baseWeight;
    private float _actionWeight;
    private float _decrementTime;
    private bool _lerpWeight;

    private float updateTime = 0;

    /// <summary>
    /// Must be called in order to correctly set all variables.
    /// </summary>
    /// <param name="baseWeight"></param>
    /// <param name="actionWeight"></param>
    /// <param name="decrementTime"></param>
    /// <param name="lerpWeight"></param>
    protected void Initialize(float actionWeight, float baseWeight = 0, float decrementTime = 1, bool lerpWeight = true)
    {
        _baseWeight = baseWeight;
        _actionWeight = actionWeight;
        _decrementTime = decrementTime;
        _lerpWeight = lerpWeight;
    }

    public void Clean()
    {
        Dirty = false;
    }

    /// <summary>
    /// Subtracts the correct weight every time the node is successfully run
    /// and sets the last time the node weight was updated to the current time.
    /// </summary>
    public void OnSuccess()
    {
        NodeWeight += _actionWeight;
        updateTime = Time.time;
    }
}