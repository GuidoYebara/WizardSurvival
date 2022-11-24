using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    // ================================ Sound Sector ================================

    public delegate void _OnPlayMusicMenu(string key);
    public static event _OnPlayMusicMenu OnPlayMusicMenu;
    public delegate void _OnStopMusicMenu();
    public static event _OnStopMusicMenu OnStopMusicMenu;

    public delegate void _OnPlaySound(string key);
    public static event _OnPlaySound OnPlaySound;
    public delegate void _OnStopSound(string key);
    public static event _OnStopSound OnStopSound;

    public delegate void _OnPlaySoundOnAS(string key, AudioSource _as);
    public static event _OnPlaySoundOnAS OnPlaySoundOnAS;
    public delegate void _OnStopSoundOnAS(AudioSource _as);
    public static event _OnStopSoundOnAS OnStopSoundOnAS;

    public static void PlayMusicMenu(string key) => OnPlayMusicMenu?.Invoke(key);
    public static void StopMusicMenu() => OnStopMusicMenu?.Invoke();

    public static void PlaySound(string key) => OnPlaySound?.Invoke(key);
    public static void StopSound(string key) => OnStopSound?.Invoke(key);

    public static void PlaySound(string key, AudioSource _as) => OnPlaySoundOnAS?.Invoke(key,_as);
    public static void StopSound(AudioSource _as) => OnStopSoundOnAS?.Invoke(_as);

    // ================================ Sound Sector ================================

    // ================================ UI Sector ================================
    public delegate void _OnUpdatePlayerLifeUI(float currentHealth, float maxHealth);
    public static event _OnUpdatePlayerLifeUI OnUpdatePlayerLifeUI;

    public delegate void _OnUpdateWaveUI(string currentWave, int currentEnemys, int maxEnemys);
    public static event _OnUpdateWaveUI OnUpdateWaveUI;   

    public delegate void _OnUpdateScoreUI(int score);
    public static event _OnUpdateScoreUI OnUpdateScoreUI;

    public delegate void _OnPlayerDeath();
    public static event _OnPlayerDeath OnPlayerDeath;
    
    public delegate void _OnEnemyDeath(Enemy enemy);
    public static event _OnEnemyDeath OnEnemyDeath;
    public static void UpdatePlayerLifeUI(float currentHealth, float maxHealth) => OnUpdatePlayerLifeUI?.Invoke(currentHealth, maxHealth);
    public static void UpdateWaveUI(string currentWave, int currentEnemys, int maxEnemys) => OnUpdateWaveUI?.Invoke(currentWave, currentEnemys, maxEnemys);
    public static void UpdateScoreUI(int score) => OnUpdateScoreUI?.Invoke(score);
    public static void TriggerOnPlayerDeath() => OnPlayerDeath?.Invoke();

    public static void TriggerOnEnemyDeath(GameObject enemy)
    {
        Enemy enemykilled = enemy.GetComponent<Enemy>();
        enemykilled?.InitializeSelf();
        OnEnemyDeath?.Invoke(enemykilled);
        UpdateScoreUI(enemykilled.Score);
    }
    // ================================ UI Sector ================================

    // ============================ General game sector ==========================
    public delegate void _OnWaveEnd(WaveSO wave);
    public static event _OnWaveEnd OnWaveEnd;

    public static void TriggerOnWaveEnd(WaveSO wave) => OnWaveEnd?.Invoke(wave);
    // ============================ General game sector ==========================

}
