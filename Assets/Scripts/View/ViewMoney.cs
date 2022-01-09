using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewMoney : MonoBehaviour
{
    [SerializeField] private Wallet _cartMoney;
    [SerializeField] private GameObject _dollarAnimation;

    private Canvas _canvas;
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _canvas = GetComponentInParent<Canvas>();
    }

    private void OnEnable()
    {
        _cartMoney.MoneyChanged += SetActualText;
        _cartMoney.MoneyChanged += StartDollarAnimation;
    }

    private void OnDisable()
    {
        _cartMoney.MoneyChanged -= SetActualText;
        _cartMoney.MoneyChanged -= StartDollarAnimation;
    }

    private void SetActualText(int count)
    {
        _text.text = count.ToString();
    }

    private void StartDollarAnimation(int count = 0)
    {
        StartCoroutine(DollarAnimation());
    }

    private IEnumerator DollarAnimation()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(_dollarAnimation, _canvas.transform);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
