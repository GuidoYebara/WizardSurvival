using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private List<EnemyType> _availableEnemys;
    private float _spawnCooldown;
    // Start is called before the first frame update
    void Start()
    {
        _availableEnemys = new List<EnemyType>();
        _availableEnemys.Add(EnemyType.BLOB);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = EnemyPool.GetInstance().GetEnemy(_availableEnemys[0]);
        newEnemy.transform.position = gameObject.transform.position;
        newEnemy.SetActive(true);
    }
}
