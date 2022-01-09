using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ColliderTake : MonoBehaviour
{
    public event UnityAction<Trash> TrashsFinded;
    public event UnityAction<TrashRare> TrashRareFinded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Trash>(out Trash trash))
            TrashsFinded?.Invoke(trash);

        if (other.TryGetComponent<TrashRare>(out TrashRare trashRare))
            TrashRareFinded?.Invoke(trashRare);
    }
}
