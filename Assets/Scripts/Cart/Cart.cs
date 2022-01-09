using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cart : MonoBehaviour
{
    [SerializeField] private int _maxCount;

    private List<Trash> _trashs = new List<Trash>();
    private List<TrashRare> _trashsRare = new List<TrashRare>();
    private PathAddTrash[] _pathsAddTrash = new PathAddTrash[30];
    private ColliderTake _takeTrash;
    private LevelUpHandler _levelUpHandler;

    public event UnityAction<int, int> CapacityChanged;
    public event UnityAction FulledCapacity;
    public event UnityAction NotFullCapacity;
    public event UnityAction<int> CountTrashRareChanged;
    public event UnityAction<TrashRare> TrashRareAdded;

    public bool HaveTrash => _trashs.Count > 0;
    public bool FullCapacity => _trashs.Count >= _maxCount;

    private void Awake()
    {
        _takeTrash = GetComponentInChildren<ColliderTake>();
        _pathsAddTrash = GetComponentsInChildren<PathAddTrash>();
        _levelUpHandler = GetComponent<LevelUpHandler>();
    }

    private void Start()
    {
        CapacityChanged?.Invoke(_trashs.Count, _maxCount);
    }

    private void OnEnable()
    {
        _takeTrash.TrashsFinded += AddTrash;
        _takeTrash.TrashRareFinded += AddTrashRare;
        _levelUpHandler.LevelUped += LevelUp;
    }

    private void OnDisable()
    {
        _takeTrash.TrashRareFinded -= AddTrashRare;
        _takeTrash.TrashsFinded -= AddTrash;
        _levelUpHandler.LevelUped -= LevelUp;
    }

    public void AddTrash(Trash trash)
    {
        if (_trashs.Contains(trash) == false && trash.OnRecycling == false && trash.Taked == false && FullCapacity == false)
        {
            _trashs.Add(trash);
            trash.StartMoveTo(_pathsAddTrash[Random.Range(0, _pathsAddTrash.Length)].transform);

            CapacityChanged?.Invoke(_trashs.Count, _maxCount);

            if (FullCapacity)
                FulledCapacity?.Invoke();
        }
    }

    public void AddTrashRare(TrashRare trashRare)
    {
        if (_trashsRare.Contains(trashRare))
            return;

        _trashsRare.Add(trashRare);
        CountTrashRareChanged?.Invoke(_trashsRare.Count);
        TrashRareAdded?.Invoke(trashRare);
    }

    public Trash TryGetTrash()
    {
        if(_trashs.Count != 0)
        {
            Trash trash = _trashs[_trashs.Count - 1];
            _trashs.Remove(trash);

            CapacityChanged?.Invoke(_trashs.Count, _maxCount);

            NotFullCapacity?.Invoke();

            return trash;
        }
        return null;
    }

    private void LevelUp(int maxCount, ColliderTake colliderTake, CartPrefab cart)
    {
        _takeTrash = colliderTake;
        _takeTrash.TrashsFinded += AddTrash;
        _takeTrash.TrashRareFinded += AddTrashRare;

        _pathsAddTrash = cart.GetComponentsInChildren<PathAddTrash>();

        for (int i = 0; i < _trashs.Count; i++)
        {
            _trashs[i].transform.position = _pathsAddTrash[Random.Range(0, _pathsAddTrash.Length)].transform.position;
            _trashs[i].transform.SetParent(_pathsAddTrash[Random.Range(0, _pathsAddTrash.Length)].transform);
        }

        _maxCount = maxCount;
    }
}
