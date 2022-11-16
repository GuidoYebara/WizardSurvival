using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class describe the movement of the enemy
 */
public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _rigidbody;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private float _forceModifier;
    
    public GameObject Player { get => _player; set => _player = value; }
    public float ForceModifier { get => _forceModifier; set => _forceModifier = value; }

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        if(_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        moveToPlayer();
    }

    void moveToPlayer()
    {
        //TODO: Adjust this to make it more "playable"
        Vector3 finalDirection =  Player.transform.position - gameObject.transform.position;
        _rigidbody.AddForce(Vector3.Normalize(finalDirection) * _forceModifier, ForceMode.VelocityChange);
    }
}
