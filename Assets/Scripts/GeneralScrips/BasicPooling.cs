using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *A basic class to allow pooling.
 *Intantiates a predefined number
 */
public class BasicPooling
{
    private List<GameObject> _pool;
    private readonly GameObject _spawnPrefab;
    private readonly GameObject _spawner;
    private int _maxAmount;

    public BasicPooling(GameObject spawnPrefab, GameObject spawner, int maxAmount)
    {
        _spawnPrefab = spawnPrefab;
        _spawner = spawner;
        _maxAmount = maxAmount;

        _pool = new List<GameObject>();
        Initialize(_maxAmount);
    }

    private void Initialize(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newSpawn = Object.Instantiate(_spawnPrefab, _spawner.transform);

            newSpawn.SetActive(false);

            _pool.Add(newSpawn);

        }
    }

    //Return
    public GameObject GetObject()
    {
        if (!IsEmpty())
        {
            GameObject current = _pool[0];
            _pool.RemoveAt(0);
            return current;
        }
        return null;
    }

    public void AddElemenToPool(GameObject objectToPool)
    {
        if(!IsFull())
            _pool.Add(objectToPool);
    }

    public void ResetPool()
    {
        _pool = new List<GameObject>();
    }

    public bool IsEmpty()
    {
        return _pool.Count == 0;
    }

    public bool IsFull()
    {
        return _pool.Count == _maxAmount;
    }

    public int MaxAmount()
    {
        return _maxAmount;
    }

    /*
     * Allows the pool to be expanded or reduced as necesary.
     * It will always set the pool to its new max.
     * Returns:
     *  - TRUE: when the operation was successful
     *  - FALSE: when it was not possible to resize the pool
     */
    public bool ChangePoolSize(int newMaxAmount)
    {
        if (_maxAmount == newMaxAmount)
        {
            return false;
        }

        int currentDiference = newMaxAmount - _pool.Count;
        if (currentDiference == 0)
        {
            _maxAmount = newMaxAmount;
            return true;
        }

        int totalDiference = newMaxAmount - _maxAmount;

        if (totalDiference > 0 || currentDiference > 0)
        { 
            //we have to add to fullfil the request
            Initialize(currentDiference);
        }
        else
        {
            _pool.RemoveRange(0, currentDiference * -1);
        }

        _maxAmount = newMaxAmount;
        return true;
    }
}