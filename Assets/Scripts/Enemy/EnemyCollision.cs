using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class should handle all collision related to an aenemy object
 */
public class EnemyCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO: send messages or something, the collision with player should be handled by the player, probably?
        if (collision.gameObject.CompareTag("Player"))
            Debug.Log("DIE SCUM!");
        if (collision.gameObject.CompareTag("Enemy"))
            Debug.Log("Im terribly sorry :(");
    }
}
