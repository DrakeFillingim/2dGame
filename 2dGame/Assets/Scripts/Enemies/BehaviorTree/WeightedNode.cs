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
        get => _nodeWeight;
        set
        {
            float toAdd = value - NodeWeight;
            if (value > 1)
            {
                toAdd = 1 - NodeWeight;
                //_nodeWeight = 1;
            }
            else if (value < 0)
            {
                //Weight = 0.2, value = -0.1
                toAdd = 0 - NodeWeight;
                //_nodeWeight = 0;
            }
            //float toAdd = (_nodeWeight > 1 || _nodeWeight < 0) ? 0 : value - NodeWeight;
            UnityEngine.Debug.Log(toAdd);
            if (toAdd != 0)
            {
                _nodeWeight += toAdd;
                Dirty = true;
            }
        }
    }

    /// <summary>
    /// Used to determine if the composite node holding a reference needs to sort its children.
    /// </summary>
    public bool Dirty { get; private set; } = true;

    private float _baseWeight = 0;
    private float _actionWeight = 0;
    private float _decrementTime = 1;
    private bool _lerpWeight = false;

    /// <summary>
    /// Must be called in order to correctly set all variables.
    /// </summary>
    /// <param name="baseWeight"></param>
    /// <param name="actionWeight"></param>
    /// <param name="decrementTime"></param>
    /// <param name="lerpWeight"></param>
    protected void Initialize(float baseWeight, float actionWeight, float decrementTime, bool lerpWeight)
    {
        _baseWeight = baseWeight;
        _actionWeight = actionWeight;
        _decrementTime = decrementTime;
        _lerpWeight = lerpWeight;

        NodeWieghtUpdater.AddNodeWieght(OnUpdate);
    }

    public void Clean()
    {
        Dirty = false;
    }

    /// <summary>
    /// Subtracts the correct weight every time the node is successfully run.
    /// </summary>
    public void OnSuccess()
    {
        NodeWeight += _actionWeight;
    }

    /// <summary>
    /// Used automatically for passively updating the weight of the node.
    /// </summary>
    private void OnUpdate()
    {
        UnityEngine.Debug.Log("node in update");
    }
}