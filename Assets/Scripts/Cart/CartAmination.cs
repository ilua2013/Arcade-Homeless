using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CartAmination : MonoBehaviour
{
    private Animator _animator;

    private const string CartRise = "CartRise";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play(CartRise);
    }
}
