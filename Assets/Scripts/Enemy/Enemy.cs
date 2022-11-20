using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WIP: this class should be used to model enemies
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyType _type;
    private string _insult;
    private int healthPoints;
    private int maxHealthPoints;
    private Vector3 _orignalSpawnPoint;

    public EnemyType Type { get => _type; set => _type = value; }

    public void InitializeSelf()
    {
        gameObject.SetActive(false);
        healthPoints = maxHealthPoints;
        gameObject.transform.position = _orignalSpawnPoint;
    }

    public string TalkShitToPlayer()
    {
        return _insult;
    }

    // Start is called before the first frame update
    void Start()
    {
        _orignalSpawnPoint = gameObject.GetComponentInParent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDeath()
    {
        EnemyPool pool = EnemyPool.GetInstance();
        if(pool != null)
        {
            InitializeSelf();
            pool.ReturnEnemyToPool(Type, gameObject);
        }
    }
}
public enum EnemyType
{
    BLOB,
    SKELLY
}