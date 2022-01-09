using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewTrashRareCount : MonoBehaviour
{
    [SerializeField] private Cart _cart;
    [SerializeField] private Animator _mushroomAnimation;

    private Canvas _canvas;
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _canvas = GetComponentInParent<Canvas>();
    }

    private void OnEnable()
    {
        _cart.CountTrashRareChanged += SetActualText;
        _cart.TrashRareAdded += HandlerAnimation;
    }

    private void OnDisable()
    {
        _cart.CountTrashRareChanged -= SetActualText;
        _cart.TrashRareAdded -= HandlerAnimation;
    }

    private void SetActualText(int count)
    {
        _text.text = $"{count}";
    }

    private void HandlerAnimation(TrashRare trashRare)
    {
        trashRare.Died += StartAnimationMushroom;
    }

    private void StartAnimationMushroom(TrashRare trashRare)
    {
        Instantiate(_mushroomAnimation, _canvas.transform);

        trashRare.Died -= StartAnimationMushroom;
    }
}
