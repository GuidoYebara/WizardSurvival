using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic game logic.
/// Should control the waves, the score and what not.
/// </summary>
public class GameController : MonoBehaviour
{
    private const string SPAWN_TAG = "Respawn";

    [SerializeField]
    private List<WaveSO> _waves;
    public List<WaveSO> Waves { get => _waves; set => _waves = value; }
    private WaveController WaveControl;

    private int _currentWave;

    // Start is called before the first frame update
    void Start()
    {
        _currentWave = 0;

        List<GameObject> AllSpawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag(SPAWN_TAG));
        AllSpawnPoints.ForEach(sp => sp.SetActive(false));

        WaveControl = gameObject.GetComponent<WaveController>();
        WaveControl.AllSpawnPoints = AllSpawnPoints;
        StartCoroutine(PrepareWave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerable WavesCoroutine()
    {
       yield return new WaitForSeconds(1);
    }

    IEnumerator PrepareWave()
    {
        WaveSO currentWave = Waves[_currentWave];
        WaveControl.Wave = currentWave;
        //Preparation time before the wave starts. 
        //At this point we could inform the UI to show a count_down or something
        yield return new WaitForSeconds(currentWave.CoolDownBeforeStart);

        WaveControl.KickOffWave();
    }
}
