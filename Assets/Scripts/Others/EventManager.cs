using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class defines events, what methods can subscribe and when these events are triggered
/// </summary>
public class EventManager : MonoBehaviour
{
    public delegate void PlayerDead();
    
    public static event PlayerDead OnPlayerDead;

    // Update is called once per frame
    void Update()
    {
        
    }
}
