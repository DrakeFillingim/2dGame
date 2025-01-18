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

    private SpriteRenderer _renderer;
    private Transform _player;
        
    private Node _root;
    private Node parryLeaf;
    private Node dashLeaf;

    private bool _playerAttacking = false;
    private Timer _playerAttackTimer;

    private void Start()
    {
        _getDirection = () => _direction;
        _renderer = GetComponent<SpriteRenderer>();
        _player = GameObject.Find("Player").transform;

        parryLeaf = new ParryLeaf("Parry", gameObject, new Node.Weight(.3f, decrementTime: 3, lerpWeight: false));
        parryLeaf.NodeSuccess += () => _playerAttacking = false;

        dashLeaf = new DashLeaf(DashDistance, DashTime, _dashCurve, _getDirection, "Dash", gameObject, new Node.Weight(1, 0.5f, lerpWeight: false));

        _root = new PrioritizedSelectorNode(new Node[]
        {
            new PrioritizedSequenceNode(new Node[]
            {
                new InverterNode(new CheckFloatLeaf(() => (_player.position - transform.position).sqrMagnitude, 9)),
                new PrioritizedSelectorNode(new Node[]
                {
                    new PrioritizedSequenceNode(new Node[]
                    {
                        new CheckBoolLeaf(() => _playerAttacking),
                        new WeightedSelectorNode(new Node[]
                        {
                            parryLeaf,
                            dashLeaf,
                        }),
                    }),
                    new PrioritizedSequenceNode(new Node[]
                    {
                        new LightAttackLeaf("Attack 1", () => _playerAttacking, gameObject),
                        new LightAttackLeaf("Attack 2", () => _playerAttacking, gameObject),
                    }),
                }),
            }),
            new IdleLeaf("Idle", gameObject),
        });

        _playerAttackTimer = Timer.CreateTimer(gameObject, "Player Attack Timer", () => _playerAttacking = false, .2f);
        GameObject.Find("Player").GetComponent<StateController>().PlayerStateChange += OnPlayerStateChange;
    }

    private void FixedUpdate()
    {
        if (transform.position.x >= _player.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            _direction = 1;
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            _direction = -1;
        }
        _root.Evaluate();
    }

    private void OnPlayerStateChange(System.Type newState)
    {
        if (newState == typeof(LightAttackState))
        {
            _playerAttacking = true;
            _playerAttackTimer.ResetTimer();
            _playerAttackTimer.StartTimer();
        }
    }
}
