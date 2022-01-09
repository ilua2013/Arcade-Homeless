using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _moneyCount;

    public event UnityAction<int> MoneyChanged;
    public event UnityAction MoneyAdded;

    public void PlusMoney(Money money)
    {
        _moneyCount += money.CountMoney;

        MoneyChanged?.Invoke(_moneyCount);
        MoneyAdded?.Invoke();
    }
}
