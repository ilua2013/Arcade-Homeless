using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMover : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Rigidbody _rigidbody;
    [Header("Speed")]
    [SerializeField] private float _speed;

    private FloatingJoystick _floatingJoystick;

    private void Start()
    {
        _floatingJoystick = GetComponent<FloatingJoystick>();
    }

    public void SetMover(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(_floatingJoystick.Direction.x, 0, _floatingJoystick.Direction.y);

        if (_floatingJoystick.Direction != Vector2.zero)
            _rigidbody.transform.right = direction;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.velocity = new Vector3(_floatingJoystick.Direction.x * _speed, 0, _floatingJoystick.Direction.y * _speed);
    }
}
