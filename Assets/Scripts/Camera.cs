using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //Properties
    public Transform player;
    public float smooth = 0.3f;

    public float height;
    public float depth;
    
    private Vector3 velocity = Vector3.zero;
    
  
    void Start()
    {
        
    }


    void Update()
    {
        Vector3 pos = new Vector3();
        var playerpos = player.position;
        
        pos.x = playerpos.x;
        pos.z = playerpos.z - depth;
        pos.y = playerpos.y + height;
        
        transform.position = pos;

    }
}
