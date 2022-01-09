using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class TrashRare : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _durationAnimation;

    private Animator _animator;
    private bool _startMove = false;

    public event UnityAction<TrashRare> Died;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_startMove == false && other.TryGetComponent<ColliderTake>(out ColliderTake collider))
            StartCoroutine(MoveTo(collider.GetComponentInChildren<PathTrashRare>().transform));
    }

    private IEnumerator MoveTo(Transform path)
    {
        while (Vector3.Distance(transform.position, path.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, path.position, _speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(StartAnimation(path));
    }

    private IEnumerator StartAnimation(Transform position)
    {
        float elappsedTime = 0;
        _animator.enabled = true;

        while(elappsedTime < _durationAnimation)
        {
            elappsedTime += Time.deltaTime;
            transform.position = position.position;

            yield return new WaitForEndOfFrame();
        }

        Died?.Invoke(this);
        Destroy(gameObject);
    }
}
