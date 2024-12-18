using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : MonoBehaviour
{
    private const float DashDistance = 5;
    private const float DashTime = 0.5f;
    private int _direction = 1;
    private System.Func<float, float, float> _dashCurve = Easings.EaseOutCubic;
    private System.Func<int> _getDirection;
    private Rigidbody2D _rb;
    private SpriteRenderer _renderer;
    private Transform _player;
        
    private Node _root;
    
    private void Start()
    {
        _getDirection = () => _direction * -1;
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _player = GameObject.Find("Player").transform;

        _root = NodeFactory.CreateCompositeNode<PrioritizedSelectorNode>(new Node[] {
            NodeFactory.CreateDecoratorNode<InverterNode>(
                NodeFactory.CreateNode<CheckFloatLeaf>(new object[] {new System.Func<float> (()=>(_player.position - transform.position).sqrMagnitude), 9 })),
            NodeFactory.CreateNode<DashLeaf>(new object[] {DashDistance, DashTime, _dashCurve, _getDirection, _rb})
        });
    }

    private void FixedUpdate()
    {
        if (_player.position.x > transform.position.x)
        {
            _renderer.flipX = false;
            _direction = 1;
        }
        else
        {
            _renderer.flipX = true;
            _direction = -1;
        }
        _root.Evaluate();
    }
}
