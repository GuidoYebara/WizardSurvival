using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *This class should be use to control the poolof enemies. The idea is to have an scalable and effient use of Enemy instances regardles of  the enemy type
 *
 */
public class EnemyPool
{
    private static EnemyPool _instance;
    private Dictionary<EnemyType, BasicPooling> _enemyPool;
    // Start is called before the first frame update
   
    private EnemyPool()
    {
        _enemyPool = new Dictionary<EnemyType, BasicPooling>();
    }

    public static EnemyPool GetInstance()
    {
        if(_instance == null)
        {
            _instance = new EnemyPool();
        }

        return _instance;
    }

    public void InitializePool(EnemyType enemyType, GameObject enemyPrefab, int amount, GameObject spawn)
    {
        Debug.Log(System.String.Format("{0}:{1}", enemyType, amount));
        if (_enemyPool.ContainsKey(enemyType))
        {
            _enemyPool.GetValueOrDefault(enemyType).ChangePoolSize(amount);
        }
        else
        { 
            _enemyPool.Add(enemyType, new BasicPooling(enemyPrefab, spawn, amount));
        }
    }


    public GameObject GetEnemy(EnemyType enemyType)
    {
        if (_enemyPool.ContainsKey(enemyType))
        {
           return _enemyPool.GetValueOrDefault(enemyType).GetObject();
        }
        return null;
    }

    public void ReturnEnemyToPool(EnemyType enemyType, GameObject enemy)
    {
        if (_enemyPool.ContainsKey(enemyType))
        {
            _enemyPool.GetValueOrDefault(enemyType).AddElemenToPool(enemy);
        }
    }

}

public enum EnemyType
{
    BLOB,
    SKELLY
}


