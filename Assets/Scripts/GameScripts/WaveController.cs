using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic wave logic.
/// This scripts controls how the waves are build and they current status
/// </summary>
public class WaveController : MonoBehaviour
{
    [SerializeField]
    private GameObject _blobPrefab, _skellyPrefab;
    private WaveSO _wave;
    private List<GameObject> _activeSpawnPoints;
    private List<GameObject> _allSpawnPoints;
    private Dictionary<EnemyType, int> currentEnemiesOnWave;

    public GameObject BlobPrefab { get => _blobPrefab; set => _blobPrefab = value; }
    public GameObject SkellyPrefab { get => _skellyPrefab; set => _skellyPrefab = value; }
    public WaveSO Wave { get => _wave; set => _wave = value; }
    private List<GameObject> ActiveSpawnPoints { get => _activeSpawnPoints; set => _activeSpawnPoints = value; }
    public List<GameObject> AllSpawnPoints { get => _allSpawnPoints; set => _allSpawnPoints = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsWaveFinished())
        {
            EndWave();
        }
    }
    private void LoadSpawnpoints()
    {
        if (ActiveSpawnPoints == null || ActiveSpawnPoints.Count == 0)
        {
            ActiveSpawnPoints = new List<GameObject>();
            if (AllSpawnPoints != null && AllSpawnPoints.Count > Wave.MaxActiveSpawnPoints)
            {
                for (int i = 0; i < Wave.MaxActiveSpawnPoints; i++)
                {
                    int selectedSpawn = Random.Range(0, AllSpawnPoints.Count);
                    ActiveSpawnPoints.Add(AllSpawnPoints[selectedSpawn]);
                    AllSpawnPoints.RemoveAt(selectedSpawn);
                }
            }
            else
            {
                ActiveSpawnPoints = AllSpawnPoints;
            }
        }
    }

    private void WaveStart()
    {
        currentEnemiesOnWave = new Dictionary<EnemyType, int>();
        EnemyPool instance = EnemyPool.GetInstance();

        if (Wave.MaxBlobOnWave > 0)
        {
            Debug.Log("initialize blob");
            instance.InitializePool(EnemyType.BLOB, BlobPrefab, Wave.MaxBlobOnScreen, gameObject);
            currentEnemiesOnWave.Add(EnemyType.BLOB, Wave.MaxBlobOnWave);
        }

        if (Wave.MaxSkellyOnWave > 0)
        {
            Debug.Log("initialize skelly");
            instance.InitializePool(EnemyType.SKELLY, SkellyPrefab, Wave.MaxSkellyOnScreen, gameObject);
            currentEnemiesOnWave.Add(EnemyType.SKELLY, Wave.MaxSkellyOnWave);
        }

        foreach (GameObject spawn in ActiveSpawnPoints)
        {
            //this is a simple solution, we should  search for a more robust one
            //we should find a way to determinate how many enemies each spawn should spawn...
            //This solutions wokrs, but its not perfect. Speciually if we start doing variable spawn points
            //We could event "move" the spawn points to add more scrambling of enemie movement
            EnemySpawn enemySpawn = spawn.GetComponent<EnemySpawn>();
            if(enemySpawn != null)
            {
                Dictionary<EnemyType, int>  availableEnemies = new Dictionary<EnemyType, int>(); 
                if (Wave.MaxBlobOnWave > 0)
                {
                    availableEnemies.Add(EnemyType.BLOB, Wave.MaxBlobOnWave / ActiveSpawnPoints.Count);
                }

                if (Wave.MaxSkellyOnWave > 0)
                {
                    availableEnemies.Add(EnemyType.SKELLY, Wave.MaxSkellyOnWave / ActiveSpawnPoints.Count);
                }

                spawn.SetActive(true);
                enemySpawn.AvailableEnemies = availableEnemies;
                enemySpawn.StartSpawn();
            }
            
        }
    }

    private void EndWave()
    {
        foreach (GameObject spawn in ActiveSpawnPoints)
        {
            spawn.SetActive(false);
        }
        ActiveSpawnPoints = null;
        currentEnemiesOnWave = null;
        //gameObject.SetActive(false);
        //then sends a message to its subscriers "WaveFinished"
    }

    public void KickOffWave()
    {
        LoadSpawnpoints();
        if (ActiveSpawnPoints != null && ActiveSpawnPoints.Count > 0)
        {
            WaveStart();
        }
    }

    void OnEnemyDeath(GameObject enemyInstance)
    {
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        if(currentEnemiesOnWave != null && currentEnemiesOnWave.ContainsKey(enemy.Type))
        {
            currentEnemiesOnWave[enemy.Type]--;
        }
    }

    public bool IsWaveFinished()
    {
        if (currentEnemiesOnWave != null && currentEnemiesOnWave.Count > 0)
        {
            foreach (EnemyType type in currentEnemiesOnWave.Keys)
            {
                if(currentEnemiesOnWave[type] <= 0)
                {
                    currentEnemiesOnWave.Remove(type);
                }
            }
            return currentEnemiesOnWave.Count == 0;
        }
        return true;
    }
}
