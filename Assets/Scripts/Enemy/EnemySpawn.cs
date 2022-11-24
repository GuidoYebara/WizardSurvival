using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //private List<EnemyType> _availableEnemys;
    [SerializeField]
    private float _spawnCooldown;
    [SerializeField]
    private Dictionary<EnemyType, int> _availableEnemies;

    public float SpawnCooldown { get => _spawnCooldown; set => _spawnCooldown = value; }
    public Dictionary<EnemyType, int> AvailableEnemies 
    { 
        get => _availableEnemies;

        set
        {
            _availableEnemies = value;
            enemyTypesToSpawn = new List<EnemyType>(AvailableEnemies.Keys); //this was done because im not sure if the SetActive() functions forces the 'Start' to be executed again
        }
    }

    private List<EnemyType> enemyTypesToSpawn;

    void OnEnable()
    {
        EventManager.OnPlayerDeath += StopSpawn;
    }
        
    void OnDisable()
    {
        EventManager.OnPlayerDeath -= StopSpawn;
    }
    
    private void SpawnEnemy()
    {
        //We could optimize this if we know how many enemies are currently deployed
        //if all the enemies are deployed, and we have to wait until new enemis can be generated
        //we could just skip the whole function
        if(enemyTypesToSpawn == null || enemyTypesToSpawn.Count == 0)
        {
            Debug.Log("We are trying to spawn non existent enemies");
            return;
        }

        EnemyType selectedEnemyType;
        if (enemyTypesToSpawn.Count > 1)
        {
            selectedEnemyType = enemyTypesToSpawn[Random.Range(0, enemyTypesToSpawn.Count)];
        } 
        else
        {
            selectedEnemyType = enemyTypesToSpawn[0];
        }

        if (ShouldSpawnEnemy(selectedEnemyType))
        {
            GameObject newEnemy = EnemyPool.GetInstance().GetEnemy(selectedEnemyType);
            if (newEnemy != null)
            {
                AvailableEnemies[selectedEnemyType] = AvailableEnemies[selectedEnemyType] - 1;
                newEnemy.transform.position = gameObject.transform.position + new Vector3(0, 0.5f, 0);
                newEnemy.SetActive(true);
            }
            else
            {
                Debug.Log("No more enemies in pool.");
            }
        }
        else
        {
            Debug.Log("EnemyRemoved");
            enemyTypesToSpawn.Remove(selectedEnemyType);
        }
        
    }

    private bool ShouldSpawnEnemy(EnemyType enemyType)
    {
        return AvailableEnemies[enemyType] > 0;
    }

    /// <summary>
    /// This method is used to determinate if the spawn should spawn more enemies 
    /// </summary>
    /// <returns>
    /// - TRUE: when there are available types
    /// - FALSE: when there are no more available types
    /// </returns>
    private bool AreThereMoreEnemies()
    {
        return enemyTypesToSpawn.Count > 0;
    }

    IEnumerator RespawnEnemys()
    {
        while (AreThereMoreEnemies())
        {
            SpawnEnemy();
            yield return new WaitForSeconds(SpawnCooldown);
        }
        Debug.Log("No more enemies to spawn");
    }

    /// <summary>
    /// When the player dies, we stop spawning enemies
    /// </summary>
    public void StopSpawn()
    {
        StopCoroutine(RespawnEnemys());
    }

    public void StartSpawn()
    {
        StartCoroutine(RespawnEnemys());
    }
}
