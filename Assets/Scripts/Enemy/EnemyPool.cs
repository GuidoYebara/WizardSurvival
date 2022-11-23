using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *This class should be use to control the poolof enemies. The idea is to have an scalable and effient use of Enemy instances regardles of  the enemy type
 *
 */
public class EnemyPool
{
    private static EnemyPool instance;
    private Dictionary<EnemyType, BasicPooling> enemyPool;
    // Start is called before the first frame update
    
    void OnEnable()
    {
        EventManager.OnEnemyDeath += StartReturnEnemyToPool;
    }
        
    void OnDisable()
    {
        EventManager.OnEnemyDeath -= StartReturnEnemyToPool;
    }
   
    private EnemyPool()
    {
        enemyPool = new Dictionary<EnemyType, BasicPooling>();
    }

    public static EnemyPool GetInstance()
    {
        if(instance == null)
        {
            instance = new EnemyPool();
        }

        return instance;
    }

    public void InitializePool(EnemyType enemyType, GameObject enemyPrefab, int amount, GameObject spawn)
    {
        if (enemyPool.ContainsKey(enemyType))
        {
            enemyPool.GetValueOrDefault(enemyType).ChangePoolSize(amount);
        }
        else
        { 
            enemyPool.Add(enemyType, new BasicPooling(enemyPrefab, spawn, amount));
        }
    }


    public GameObject GetEnemy(EnemyType enemyType)
    {
        if (enemyPool.ContainsKey(enemyType))
        {
           return enemyPool.GetValueOrDefault(enemyType).GetObject();
        }
        return null;
    }

    public void ReturnEnemyToPool(EnemyType enemyType, GameObject enemy)
    {
        if (enemyPool.ContainsKey(enemyType))
        {
            enemyPool.GetValueOrDefault(enemyType).AddElemenToPool(enemy);
        }
    }

    /// <summary>
    /// Method subscribed to enemy's death.
    /// Returns the enemy to it's pool
    /// </summary>
    /// <param name="enemyInstance">The enemy that just died</param>
    public void StartReturnEnemyToPool(Enemy enemy)
    {
        if(enemy != null)
        {
            ReturnEnemyToPool(enemy.Type, enemy.gameObject);
        }
    }
}


