using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Fabric : MonoBehaviour
{
    [SerializeField] private Money _moneyPrefab;
    [Header("Times")]
    [SerializeField] private float _timeRecycling;
    [SerializeField] private float _timeAddTrash;
    [Header("Transforms")]
    [SerializeField] private Transform _pathAddTrash;
    [SerializeField] private Transform _pathRecycling;
    [SerializeField] private Transform _pathCreateMoney;

    private List<Trash> _trashs = new List<Trash>();
    private bool _startRecycling = false;
    private bool _startAddTrash = false;

    private void Update()
    {
        if(_trashs.Count != 0 && _startRecycling == false && _trashs[_trashs.Count - 1].StartMove == false)
            StartCoroutine(RecyclingTrash());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<ObjectMover>(out ObjectMover objectMover) && _startAddTrash != true)
        {
            if (objectMover.GetComponentInParent<Cart>().HaveTrash)
            {
                Trash trash = objectMover.GetComponentInParent<Cart>().TryGetTrash();

                if (trash.OnRecycling == false)
                    StartCoroutine(AddTrash(trash));
            }
        }
    }

    private IEnumerator AddTrash(Trash trash)
    {
        _startAddTrash = true;

        _trashs.Add(trash);
        trash.SetOnRecycling();
        trash.StartMoveTo(_pathAddTrash);
        trash.SetTarget(_pathAddTrash);

        yield return new WaitForSeconds(_timeAddTrash);
        _startAddTrash = false;
    }

    private IEnumerator RecyclingTrash()
    {
        _startRecycling = true;

        Trash currentTrash = _trashs[_trashs.Count - 1];
        currentTrash.StartMoveTo(_pathRecycling);
        _trashs.RemoveAt(_trashs.Count - 1);

        yield return new WaitForSeconds(_timeRecycling);

        InstatiateMoney();

        Destroy(currentTrash.gameObject);

        _startRecycling = false;
    }

    private void InstatiateMoney()
    {
        Money money = Instantiate(_moneyPrefab, _pathCreateMoney.position, _moneyPrefab.transform.rotation);
    }
}
