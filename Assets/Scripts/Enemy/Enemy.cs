using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private EnemyType _type;
    private string _insult;
    private int healthPoints;
    private int maxHealthPoints;
    private GameObject spawnPoint;

    public void InitializeSelf()
    {
        gameObject.SetActive(false);
        healthPoints = maxHealthPoints;
        gameObject.transform.position = spawnPoint.transform.position;
    }

    public string TalkShitToPlayer()
    {
        return _insult;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
