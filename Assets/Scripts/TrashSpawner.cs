using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TrashSpawner : MonoBehaviour
{
    [SerializeField] private List<Trash> _trashs = new List<Trash>();
    [SerializeField] private List<TrashRare> _trashRare = new List<TrashRare>();
    [SerializeField] private float _delaySpawn;

    private Transform[] _pathsSpawn = { };
    private float _elapsedTime;
    private int _indexPathSpawn;

    private void Start()
    {
        _pathsSpawn = GetComponentsInChildren<Transform>();

        for (int i = 0; i < _pathsSpawn.Length; i++)
            SpawnTrash();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime > _delaySpawn)
        {
            SpawnTrash();
            _elapsedTime = 0;
        }
    }

    private void SpawnTrash()
    {
        _indexPathSpawn++;
        int percent = Random.Range(1, 101);

        if (_indexPathSpawn == _pathsSpawn.Length - 1)
            _indexPathSpawn = 0;

        if (percent >= 95)
        {
            TrashRare trashRare = _trashRare[Random.Range(0, _trashRare.Count)];
            Vector3 position = _pathsSpawn[_indexPathSpawn].position;

            Instantiate(trashRare, position, trashRare.transform.rotation).transform.SetParent(transform);
            return;
        }

        Trash trash = _trashs[Random.Range(0, _trashs.Count)];
        Vector3 pathsSpawn = _pathsSpawn[_indexPathSpawn].position;

        Instantiate(trash, pathsSpawn, trash.transform.rotation).transform.SetParent(transform);
    }
}
