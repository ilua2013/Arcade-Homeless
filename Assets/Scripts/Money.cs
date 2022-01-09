using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Money : MonoBehaviour
{
    [SerializeField] private int _countMoney;
    [SerializeField] private Vector3 _forceStart;

    public int CountMoney => _countMoney;

    private void OnEnable()
    {
        Vector3 forceAtPosition = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        GetComponent<Rigidbody>().AddForceAtPosition(_forceStart, forceAtPosition ,ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<Wallet>() != null)
        {
            other.GetComponentInParent<Wallet>().PlusMoney(this);
            Destroy(gameObject);
        }
    }
}
