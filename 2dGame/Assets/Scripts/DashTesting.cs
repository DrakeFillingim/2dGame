using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTesting : MonoBehaviour
{
    public float distance = 10;
    public bool usePosition = true;

    private float _currentTime = 0;
    private float _maxTime = .5f;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (usePosition)
        {
            _currentTime += Time.deltaTime;
            transform.position = Vector2.Lerp(Vector2.zero, new Vector2(distance, 0), Easings.EaseOutQuint(Mathf.Clamp(_currentTime, 0, _maxTime), _maxTime));
        }
    }

    private void FixedUpdate()
    {
        if (!usePosition)
        {
            if (_currentTime <= _maxTime)
            {
                
                //_rb.velocity = new Vector2(distance * ((5 / _maxTime) * Mathf.Pow(1 - (_currentTime / _maxTime), 4)), 0);
                _rb.velocity = new Vector2(distance * DerivateCalculator.FivePointDerive(Easings.EaseOutQuint, _currentTime, _maxTime), 0);
                _currentTime += Time.fixedDeltaTime;

            }
            else
            {
                _rb.velocity = Vector2.zero;
            }
        }
    }
}
