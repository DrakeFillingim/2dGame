using UnityEngine;

public class DashLeaf : LeafNode
{
    private float _dashDistance = 0;
    private float _dashTime = 0;
    private float _currentDashTime = 0;
    private System.Func<float, float, float> _dashCurve;
    private System.Func<int> _getDirection;
    private int _direction;

    private Rigidbody2D _rb;
    private int _animationHash;

    private Vector2 _startPosition;
    private bool _firstFrame = true;

    public DashLeaf(float dashDistance, float dashTime, System.Func<float, float, float> dashCurve, System.Func<int> direction,
        string animationName, GameObject agent, Weight weightComponent = null) : base(agent, weightComponent)
    {
        _dashDistance = dashDistance;
        _dashTime = dashTime;
        _dashCurve = dashCurve;
        _getDirection = direction;
        _rb = agent.GetComponent<Rigidbody2D>();
        _animationHash = Animator.StringToHash(animationName);
    }

    public override NodeStates Evaluate()
    {
        if (_firstFrame)
        {
            _animator.Play(_animationHash);
            _direction = _getDirection();
            _startPosition = _rb.transform.position;
            _firstFrame = false;
        }
        _rb.MovePosition(new Vector2(_startPosition.x + (_direction * _dashDistance * _dashCurve(_currentDashTime, _dashTime)), _startPosition.y));
        _currentDashTime += Time.deltaTime;
        if (_currentDashTime >= _dashTime)
        {
            RaiseNodeSuccess();
            _currentDashTime = 0;
            _firstFrame = true;
            return NodeStates.Success;
        }
        return NodeStates.Running;
    }
}
