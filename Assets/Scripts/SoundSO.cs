using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "NameOfSound", menuName = "New Sound")]

public class SoundSO : ScriptableObject
{
    public string keyCode;
    public AudioClip clip;

    [Range(0f, 256f)]
    public int priority;
    [Range(-3f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float spatialBlend;

    public float minDistance;
    public float maxDistance;

    public bool loop;
}
