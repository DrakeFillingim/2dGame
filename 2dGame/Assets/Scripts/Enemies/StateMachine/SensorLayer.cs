using System;
using UnityEngine;

public class SensorLayer : MonoBehaviour
{
    public event Action PlayerAttack;
    
    private Transform _player;


    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        _player = player.transform;
        player.GetComponent<StateController>().PlayerStateChange += OnPlayerStateChange;
        PlayerAttack += OnPlayerAttack;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnPlayerStateChange(Type newState)
    {
        if (newState == typeof(LightAttackState))
        {
            if ((_player.position - transform.position).sqrMagnitude < 9 && _player.localScale.x != transform.localScale.x)
            {
                PlayerAttack?.Invoke();
            }
        }
    }

    private void OnPlayerAttack()
    {
        print("player attack");
    }
}
