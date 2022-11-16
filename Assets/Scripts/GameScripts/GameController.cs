using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Basic game logic.
 * WIP: this should controll general game behaviour, like waes and stuff
 */
public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _blobPrefab, _skellyPrefab;
    public GameObject BlobPrefab { get => _blobPrefab; set => _blobPrefab = value; }
    public GameObject SkellyPrefab { get => _skellyPrefab; set => _skellyPrefab = value; }

    [SerializeField]
    private List<GameObject> _spawnPoints;
    public List<GameObject> SpawnPoints { get => _spawnPoints; set => _spawnPoints = value; }

    //Wave example: this is for testing, should be change to something more robust
    [SerializeField]
    public int maxBlobOnWave, maxSkellyOnWave, maxBlobOnScreen, maxSkellyOnScreen;
    [SerializeField]
    public List<EnemyType> enemiesOnWave;

    // Start is called before the first frame update
    void Start()
    {
        //This is an example of how waves may be handled;
        //waves could be scriptable objects and the game master could just have a list of waves and use their propertys to set up the wave
        if(enemiesOnWave == null || enemiesOnWave.Count == 0)
        {
            enemiesOnWave = new List<EnemyType>();
            enemiesOnWave.Add(EnemyType.BLOB);
        }

        EnemyPool instance = EnemyPool.GetInstance();

        if (enemiesOnWave.Contains(EnemyType.BLOB))
        {
            instance.InitializePool(EnemyType.BLOB, BlobPrefab, maxBlobOnScreen, gameObject);
        }

        if (enemiesOnWave.Contains(EnemyType.SKELLY))
        {
            instance.InitializePool(EnemyType.SKELLY, SkellyPrefab, maxSkellyOnScreen, gameObject);
        }

        if(SpawnPoints != null && SpawnPoints.Count > 0)
        {
            foreach (GameObject spawn in SpawnPoints)
            {
                //this is a simple solution, we should  search for a more robust one
                //we should find a way to determinate how many enemies each spawn should spawn... This solutions wokrs, but its not perfect. Speciually if we start doing variable spawn points
                //We could event "move" the spawn points to add mor scrambling of enemie movement
                spawn.GetComponent<EnemySpawn>().AvailableEnemies = new Dictionary<EnemyType, int>();
                if (enemiesOnWave.Contains(EnemyType.BLOB))
                {
                    spawn.GetComponent<EnemySpawn>().AvailableEnemies.Add(EnemyType.BLOB, maxBlobOnWave/SpawnPoints.Count);
                }

                if (enemiesOnWave.Contains(EnemyType.SKELLY))
                {
                    spawn.GetComponent<EnemySpawn>().AvailableEnemies.Add(EnemyType.SKELLY, maxSkellyOnWave / SpawnPoints.Count);
                }

                spawn.SetActive(true); 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
