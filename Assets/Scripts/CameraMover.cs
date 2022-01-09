using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _speedToStartPosition;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private Transform _pursued;
    [SerializeField] private Vector3 _offset;
    [Header("Rootation when moving")]
    [SerializeField] private float _xRootation;
    [SerializeField] private float _yRootation;

    private Quaternion _startRotation;
    private Vector3 _direction;

    private void OnEnable()
    {
        _startRotation = transform.rotation;
        _direction = transform.position;
    }

    private void OnValidate()
    {
        transform.position = _pursued.position + _offset;
    }

    private void Update()
    {
        _direction = Vector3.MoveTowards(_direction, _pursued.position + _offset, _speedToStartPosition * Time.deltaTime);
        transform.position = _direction;

        if (_joystick.Direction == Vector2.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _startRotation, _speedToStartPosition * Time.deltaTime);
        else
            transform.rotation = Quaternion.Euler(45 + -(_xRootation * _joystick.Direction.y), _yRootation * _joystick.Direction.x, 0);
    }

    public void SetPursued(Transform pursued)
    {
        _pursued = pursued;
    }
}
