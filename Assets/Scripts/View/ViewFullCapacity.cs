using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewFullCapacity : MonoBehaviour
{
    [SerializeField] private Cart _cart;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _text.outlineWidth = 0.2f;
        _text.outlineColor = Color.red;
    }

    private void OnEnable()
    {
        _cart.FulledCapacity += EnableText;
        _cart.NotFullCapacity += DisableText;
    }

    private void OnDisable()
    {
        _cart.FulledCapacity -= EnableText;
        _cart.NotFullCapacity -= DisableText;
    }

    private void EnableText()
    {
        _text.enabled = true;
    }

    private void DisableText()
    {
        _text.enabled = false;
    }
}
