using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SoundSO[] soundList;
    [SerializeField] AudioSource musicMenu;
    [SerializeField] List<AudioSource> audioSourcesList;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSourcesList = new List<AudioSource>();
        if (gameObject.TryGetComponent(out AudioSource music)) musicMenu = music;

        EventManager.OnPlaySoundOnAS += PlaySoundOnAS;
        EventManager.OnStopSoundOnAS += StopSoundOnAS;
        EventManager.OnPlayMusicMenu += PlayMusicMenu;
        EventManager.OnStopMusicMenu += StopMusicMenu;
        EventManager.OnPlaySound += PlaySound;
        EventManager.OnStopSound += StopSound;
    }

    private void OnDestroy()
    {
        EventManager.OnPlaySoundOnAS -= PlaySoundOnAS;
        EventManager.OnStopSoundOnAS -= StopSoundOnAS;
        EventManager.OnPlayMusicMenu -= PlayMusicMenu;
        EventManager.OnStopMusicMenu -= StopMusicMenu;
        EventManager.OnPlaySound -= PlaySound;
        EventManager.OnStopSound -= StopSound;
    }

    public void PlayMusicMenu(string key)
    {
        if (musicMenu == null)
        {
            Debug.LogError($"[{gameObject.name}.PlayMusicMenu]Error: \"{gameObject}\" does not contain a AudioSource!");
            return;
        }

        SoundSO sound = null;
        for (int i = 0; i < soundList.Length; i++)
        {
            if (soundList[i].keyCode == key)
            {
                sound = soundList[i];

                musicMenu.clip = sound.clip;
                musicMenu.pitch = sound.pitch;
                musicMenu.volume = sound.volume;
                musicMenu.priority = sound.priority;
                musicMenu.spatialBlend = sound.spatialBlend;

                musicMenu.minDistance = sound.minDistance;
                musicMenu.maxDistance = sound.maxDistance;

                musicMenu.loop = sound.loop;
                musicMenu.Play();
                return;
            }
        }
        if (sound == null) Debug.LogError($"[{gameObject.name}.PlayMusicMenu]Error: \"{key}\" could not be found!");
    }
    public void StopMusicMenu() => musicMenu.Stop();

    public void PlaySound(string key)
    {
        SoundSO sound = null;
        for (int i = 0; i < soundList.Length; i++)
        {
            if (soundList[i].keyCode == key)
            {
                sound = soundList[i];
                break;
            }
        }
        for (int i = 0; i < audioSourcesList.Count; i++)
        {
            if (!audioSourcesList[i].isPlaying)
            {
                audioSourcesList[i].clip = sound.clip;
                audioSourcesList[i].spatialBlend = sound.spatialBlend;
                audioSourcesList[i].volume = sound.volume;
                audioSourcesList[i].pitch = sound.pitch;
                audioSourcesList[i].priority = sound.priority;

                audioSourcesList[i].minDistance = sound.minDistance;
                audioSourcesList[i].maxDistance = sound.maxDistance;

                audioSourcesList[i].loop = sound.loop;

                audioSourcesList[i].Play();
                return;
            }
        }
        
        if (sound == null) Debug.LogError($"[{gameObject.name}.PlaySound]Error: \"{key}\" could not be found!");
        else
        {
            AudioSource newaudio = gameObject.AddComponent<AudioSource>();
            audioSourcesList.Add(newaudio);
            newaudio.clip = sound.clip;
            newaudio.spatialBlend = sound.spatialBlend;
            newaudio.volume = sound.volume;
            newaudio.pitch = sound.pitch;
            newaudio.priority = sound.priority;

            newaudio.minDistance = sound.minDistance;
            newaudio.maxDistance = sound.maxDistance;

            newaudio.loop = sound.loop;

            newaudio.Play();
        }
    }
    public void StopSound(string key)
    {
        SoundSO sound = null;
        for (int i = 0; i < soundList.Length; i++)
        {
            if (soundList[i].keyCode == key)
            {
                sound = soundList[i];
                break;
            }
        }
        for (int i = 0; i < audioSourcesList.Count; i++)
        {
            if (audioSourcesList[i].clip == sound.clip && audioSourcesList[i].isPlaying)
            {
                audioSourcesList[i].Stop();
                return;
            }
        }
        if (sound == null) Debug.LogError($"[{gameObject.name}.StopSound]Error: \"{key}\" could not be found!");
    }

    public void PlaySoundOnAS(string key, AudioSource _as)
    {
        SoundSO sound = null;
        if (_as == null)
        {
            Debug.LogError($"[{gameObject.name}.PlaySoundOnAS]Error: Se detecto instancia null: {_as}");
            return;
        }
        for (int i = 0; i < soundList.Length; i++)
        {
            if (soundList[i].keyCode == key)
            {
                sound = soundList[i];

                _as.clip = sound.clip;
                _as.spatialBlend = sound.spatialBlend;
                _as.volume = sound.volume;
                _as.pitch = sound.pitch;
                _as.priority = sound.priority;

                _as.minDistance = sound.minDistance;
                _as.maxDistance = sound.maxDistance;

                _as.loop = sound.loop;

                _as.Play();
                return;
            }
        }
        if (sound == null) Debug.LogError($"[{gameObject.name}.PlaySoundOnAS]Error: \"{key}\" could not be found!");
    }
    public void StopSoundOnAS(AudioSource _as)
    {
        if (_as == null)
        {
            Debug.LogError($"[{gameObject.name}.StopSoundOnAS]Error: referencia null \"{_as}\"");
            return;
        }

        _as.Stop();
    }
}
