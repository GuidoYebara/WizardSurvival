using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Basic game logic.
/// Should control the waves, the score and what not.
/// </summary>
public class GameController : MonoBehaviour
{
    private const string SPAWN_TAG = "Respawn";

    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject panelGameOver;

    [SerializeField]
    private List<WaveSO> _waves;
    public List<WaveSO> Waves { get => _waves; set => _waves = value; }
    private WaveController WaveControl;

    [SerializeField]
    private int currentWave;

    void OnEnable()
    {
        EventManager.OnPlayerDeath += StopPrepareWave;
    }
        
    void OnDisable()
    {
        EventManager.OnPlayerDeath -= StopPrepareWave;
    }
    
    void Start()
    {
        GameStart();
        EventManager.OnWaveEnd += onWaveFinished;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GamePause();
        }
    }

    private void GameStart()
    {
        currentWave = 0;

        List<GameObject> AllSpawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag(SPAWN_TAG));
        AllSpawnPoints.ForEach(sp => sp.SetActive(false));

        WaveControl = gameObject.GetComponent<WaveController>();
        WaveControl.AllSpawnPoints = AllSpawnPoints;
        WaveControl.enabled = false;
        StartCoroutine(PrepareWave());
    }

    IEnumerator PrepareWave()
    {
        WaveSO nextWave = Waves[currentWave];
        WaveControl.Wave = nextWave;
        //Preparation time before the wave starts. 
        //At this point we could inform the UI to show a count_down or something
        
        yield return new WaitForSeconds(nextWave.CoolDownBeforeStart);
        WaveControl.enabled = true;
        WaveControl.KickOffWave();
    }

    void onWaveFinished(WaveSO wave)
    {
        StopCoroutine(PrepareWave()); //Not completly sure if this courutine is still going,
                                      //it should't, but, just to sure, we kill it
        WaveControl.enabled = false;
        currentWave++;
        if(currentWave < Waves.Count)
        {
            StartCoroutine(PrepareWave());
        }
        else
        {
            //The game is finished!... yay?
            //At this poit we should inform the player they won, and send them to a splash screen of victory
            //or something, maybe just close the game, or send the player to the shadow realm for crimes
            //against the poppulation of monsters they just slaugthered without mercy
            //Maybe the player was the true monster all along? 
            GamePause();
        }
    }

    /// <summary>
    /// Re-starts the game
    /// Enabling and disabling this component should have the same effect
    /// </summary>
    public void ReStartGame()
    {
        GameStart();
    }

    /// <summary>
    /// This function should decide what to do when the player dies
    /// Should we start over? just re-start the current wave?
    /// </summary>
    public void StopPrepareWave()
    {
        StopCoroutine(PrepareWave()); //Not completly sure if this courutine is still going,
                                      //it should't, but, just to sure, we kill it
        WaveControl.enabled = false;
        currentWave = 0;
    }

    public void GamePause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            panelPause.SetActive(false);
        }
        else if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            panelPause.SetActive(true);
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame() => Application.Quit();
}
