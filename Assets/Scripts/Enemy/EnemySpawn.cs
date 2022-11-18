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

    // Start is called before the first frame update
    void Start()
    {
        if (AvailableEnemies == null)
        {
            AvailableEnemies = new Dictionary<EnemyType, int>();
            AvailableEnemies.Add(EnemyType.BLOB, 10);
        }
        enemyTypesToSpawn = new List<EnemyType>(AvailableEnemies.Keys);

        StartCoroutine(RespawnEnemys());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        //We could optimize this if we know how many enemies are currently deployed
        //if all the enemies are deployed, and we have to wait until new enemis can be generated
        //we could just skip the whole function
        EnemyType selectedEnemyType = enemyTypesToSpawn[0];
        if (enemyTypesToSpawn.Count > 0)
        {
            selectedEnemyType = enemyTypesToSpawn[Random.Range(0, enemyTypesToSpawn.Count)];
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
        
    }

    private bool ShouldSpawnEnemy(EnemyType enemyType)
    {
        return AvailableEnemies[enemyType] > 0;
    }

    private bool AreThereMoreEnemies()
    {
        bool shouldSpawn = false;
        foreach (EnemyType type in enemyTypesToSpawn)
        {
            shouldSpawn = shouldSpawn || AvailableEnemies[type] > 0;
        }

        return shouldSpawn;
    }

    IEnumerator RespawnEnemys()
    {
        while (AreThereMoreEnemies())
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_spawnCooldown);
        }
    }
}
