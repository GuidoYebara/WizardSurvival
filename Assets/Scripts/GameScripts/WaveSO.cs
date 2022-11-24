using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NameOfWave", menuName = "New Wave")]

/// <summary>
/// Defines the params of each wave. 
/// For easy of use, this has a param for each know type of enemy.
/// This model could be improved to be more scalable if more enemy types are added.
/// </summary>
public class WaveSO : ScriptableObject
{
    /// <summary>
    /// It starts in Wave 1, and so on...
    /// </summary>
    public int WaveNumber;
    /// <summary>
    /// Defines the max ammount of BLOB enemies on the wave
    /// 0 should spawn NO enemies of this type
    /// </summary>
    public int MaxBlobOnWave;
    /// <summary>
    /// Defines the max ammout of SKELLY enemies on the wave
    /// 0 should spawn NO enemies of this type
    /// </summary>
    public int MaxSkellyOnWave;
    /// <summary>
    /// Defines the max ammount of blobs ON THE SCREEN AT THE SAME TIME
    /// This is both for difficulty adjustments AND performance
    /// </summary>
    public int MaxBlobOnScreen;
    /// <summary>
    /// Defines the max ammount of skellys ON THE SCREEN AT THE SAME TIME
    /// This is both for difficulty adjustments AND performance
    /// </summary>
    public int MaxSkellyOnScreen;

    /// <summary>
    /// The maximum ammount of active spawnPoints
    /// </summary>
    public int MaxActiveSpawnPoints;

    /// <summary>
    /// The name of the wave. 
    /// May be used for UI
    /// </summary>
    public string WaveName;

    /// <summary>
    /// A description for the wave.
    /// May be used to gave information to the player about the wave
    /// or just as a flavor text to add something extra or mock the player
    /// </summary>
    public string WaveDescription;

    /// <summary>
    /// This param indicates how much time (in seconds) should the player have for preparation before 
    /// the wave starts
    /// </summary>
    public int CoolDownBeforeStart;
}
