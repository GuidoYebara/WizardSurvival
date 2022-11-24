using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WIP: this class should be used to model enemies
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType _type;
    [SerializeField] private float _dmg;
    [SerializeField] private float _hitcdtimer;
    [SerializeField] private int _score;
    
    private string _insult;
    private int maxHealthPoints;
    private int healthPoints;
    private Vector3 _orignalSpawnPoint;
    private float _hitcd;
    
    public EnemyType Type { get => _type; set => _type = value; }
    public float Dmg { get => _dmg; set => _dmg = value; }
    public float Hitcdtimer { get => _hitcdtimer; set => _hitcdtimer = value; }
    public int Score { get => _score; set => _score = value; }
    
    public void InitializeSelf()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = gameObject.GetComponentInParent<Transform>().position;
        HealthSystem health = gameObject.GetComponent<HealthSystem>();
        if(health != null)
        {
            health.Revive();
        }
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
        _hitcd = (_hitcd < 0)? 0 : _hitcd-=Time.deltaTime;
    }
        
    private void OnCollisionStay(Collision collision)
    {
        
        //TODO: send messages or something, the collision with player should be handled by the player, probably?
        if (collision.gameObject.CompareTag("Player") && _hitcd<=0)
        {
            collision.gameObject.GetComponent<HealthSystem>().TakeDamage(Dmg, TypeOfSpellElement.Physical);
            _hitcd = _hitcdtimer;
        }
        
        if (collision.gameObject.CompareTag("Enemy"))
            Debug.Log("Im terribly sorry :(");
    }
}
public enum EnemyType
{
    BLOB,
    SKELLY
}