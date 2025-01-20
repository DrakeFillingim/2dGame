using System;
using UnityEngine;

public class SensorLayer : MonoBehaviour
{
    public event Action PlayerAttack;
    public event Action PlayerEntersRange;
    
    private Transform _player;

    private bool broadcasted = false;

    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        _player = player.transform;
        player.GetComponent<StateController>().PlayerStateChange += OnPlayerStateChange;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if ((_player.position - transform.position).sqrMagnitude < 9 && !broadcasted)
        {
            broadcasted = true;
            PlayerEntersRange?.Invoke();
        }
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
}
