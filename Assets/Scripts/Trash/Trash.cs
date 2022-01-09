using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Trash : MonoBehaviour, ITakebale
{
    [SerializeField] private float _speed;
    [SerializeField] private float _verticalMove;

    private Transform _target;
    private Rigidbody _rigidbody;
    private Collider _collider;

    public bool Taked { get; private set; } = false;

    public bool OnRecycling { get; private set; }
    public bool StartMove { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (_target != null && Vector3.Distance(transform.position, _target.position) > 0.2f)
            StartMoveTo(_target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Trash>(out Trash trash) && Taked == false && OnRecycling == false && trash.Taked == false && trash.OnRecycling == false)
            transform.position = transform.position + new Vector3(Random.Range(0.5f, 4), 0, Random.Range(0.5f, 4));
    }

    public void StartMoveTo(Transform targetPath)
    {
        Taked = true;

        if (StartMove == false)
        {
            if (_collider != null)
                _collider.isTrigger = true;

            StartCoroutine(MoveTo(targetPath));
        }
    }

    public IEnumerator MoveTo(Transform targetPath)
    {
        StartMove = true;
        _target = targetPath;

        float moveX = transform.position.x;
        float moveY = 0;
        float maxY = transform.position.y + _verticalMove;
        float moveZ = transform.position.z;

        float firstDistance = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(_target.position.x, 0, _target.position.z));
        float currentDistance = firstDistance;

        while (Vector3.Distance(transform.position, _target.position) > 0.2f)
        {
            currentDistance = Vector3.Distance(
                new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(_target.position.x, 0, _target.position.z));

            if (currentDistance / firstDistance > 0.5f)
                moveY = Mathf.MoveTowards(transform.position.y, maxY, _speed * Time.deltaTime);
            else
                moveY = Mathf.MoveTowards(transform.position.y, _target.position.y, _speed * Time.deltaTime);

            
            moveX = Mathf.MoveTowards(moveX, _target.position.x, _speed * Time.deltaTime);
            moveZ = Mathf.MoveTowards(moveZ, _target.position.z, _speed * Time.deltaTime);

            transform.position = new Vector3(moveX, moveY, moveZ);
            yield return new WaitForFixedUpdate();
        }

        transform.SetParent(_target);

        _rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        StartMove = false;
    }

    public void SetOnRecycling()
    {
        OnRecycling = true;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
