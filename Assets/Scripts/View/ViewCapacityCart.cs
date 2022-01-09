using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewCapacityCart : MonoBehaviour
{
    [SerializeField] private Cart _cart;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _cart.CapacityChanged += SetActualText;
    }

    private void OnDisable()
    {
        _cart.CapacityChanged -= SetActualText;
    }

    private void SetActualText(int count, int maxCount)
    {
        _text.text = $"{count} / {maxCount}";
    }
}
