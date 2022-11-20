using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
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
}
