using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

[RequireComponent(typeof(Cart))]
public class LevelUpHandler : MonoBehaviour
{
    [SerializeField] private JoystickMover _joystick;
    [SerializeField] private CartPrefab[] _carts;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private int[] _pointToNextLevel;
    [SerializeField] private int[] _maxCapacity;

    private Wallet _wallet;
    private int _currentPoint;
    private int _currentLevel;

    public event UnityAction<int, ColliderTake, CartPrefab> LevelUped;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
    }

    private void OnEnable()
    {
        _wallet.MoneyAdded += PlusPointNextLevel;
    }

    private void OnDisable()
    {
        _wallet.MoneyAdded -= PlusPointNextLevel;
    }

    private void PlusPointNextLevel()
    {
        _currentPoint++;

        if ((_currentLevel + 1) < _pointToNextLevel.Length && _currentPoint >= _pointToNextLevel[_currentLevel + 1])
            LevelUp();
    }

    private void LevelUp()
    {
        _currentLevel++;

        Vector3 root = GetComponentInChildren<ObjectMover>().transform.rotation.eulerAngles;
        Vector3 currentPosition = GetComponentInChildren<ObjectMover>().transform.position;
        Destroy(GetComponentInChildren<CartPrefab>().gameObject);

        CartPrefab cart = Instantiate(_carts[_currentLevel], transform, false);
        Transform cartTransform = cart.GetComponentInChildren<ObjectMover>().transform;

        cart.transform.Rotate(root);
        cart.transform.position = currentPosition;

        _camera.LookAt = cart.GetComponentInChildren<ObjectMover>().transform;
        _camera.Follow = cart.GetComponentInChildren<ObjectMover>().transform;

        _joystick.SetMover(cartTransform.GetComponent<Rigidbody>());

        LevelUped?.Invoke(_maxCapacity[_currentLevel], cart.GetComponentInChildren<ColliderTake>(), cart);
    }
}
