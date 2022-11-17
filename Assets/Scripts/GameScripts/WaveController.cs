using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic wave logic.
/// This scripts controls how the wave are build.
/// </summary>
public class WaveController : MonoBehaviour
{
    [SerializeField]
    private GameObject _blobPrefab, _skellyPrefab;
    public GameObject BlobPrefab { get => _blobPrefab; set => _blobPrefab = value; }
    public GameObject SkellyPrefab { get => _skellyPrefab; set => _skellyPrefab = value; }

    private WaveSO _wave;
    public WaveSO Wave { get => _wave; set => _wave = value; }

    private List<GameObject> _activeSpawnPoints;
    private List<GameObject> ActiveSpawnPoints { get => _activeSpawnPoints; set => _activeSpawnPoints = value; }

    private List<GameObject> _allSpawnPoints;
    public List<GameObject> AllSpawnPoints { get => _allSpawnPoints; set => _allSpawnPoints = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
        EnemyPool instance = EnemyPool.GetInstance();

        if (Wave.MaxBlobOnWave > 0)
        {
            Debug.Log("initialize blob");
            instance.InitializePool(EnemyType.BLOB, BlobPrefab, Wave.MaxBlobOnScreen, gameObject);
        }

        if (Wave.MaxSkellyOnWave > 0)
        {
            Debug.Log("initialize skelly");
            instance.InitializePool(EnemyType.SKELLY, SkellyPrefab, Wave.MaxSkellyOnScreen, gameObject);
        }

        foreach (GameObject spawn in ActiveSpawnPoints)
        {
            //this is a simple solution, we should  search for a more robust one
            //we should find a way to determinate how many enemies each spawn should spawn...
            //This solutions wokrs, but its not perfect. Speciually if we start doing variable spawn points
            //We could event "move" the spawn points to add mor scrambling of enemie movement
            spawn.GetComponent<EnemySpawn>().AvailableEnemies = new Dictionary<EnemyType, int>();
            if (Wave.MaxBlobOnWave > 0)
            {
                spawn.GetComponent<EnemySpawn>().AvailableEnemies.Add(EnemyType.BLOB, Wave.MaxBlobOnWave / ActiveSpawnPoints.Count);
            }

            if (Wave.MaxSkellyOnWave > 0)
            {
                spawn.GetComponent<EnemySpawn>().AvailableEnemies.Add(EnemyType.SKELLY, Wave.MaxSkellyOnWave / ActiveSpawnPoints.Count);
            }

            spawn.SetActive(true);
        }
    }

    private void WaveEnd()
    {
        foreach (GameObject spawn in ActiveSpawnPoints)
        {
            spawn.SetActive(false);
        }
        ActiveSpawnPoints = null;
        //gameObject.SetActive(false);
    }

    public void KickOffWave()
    {
        LoadSpawnpoints();
        // TODO: find a way to use the Wave delay at this point... maybe a coroutine?
        if (ActiveSpawnPoints != null && ActiveSpawnPoints.Count > 0)
        {
            WaveStart();
        }
    }
}
