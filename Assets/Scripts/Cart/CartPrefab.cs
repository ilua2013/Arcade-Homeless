using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CartPrefab : MonoBehaviour
{
    public event UnityAction Created;

    private void OnEnable()
    {
        Created?.Invoke();
    }
}
