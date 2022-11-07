using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [SerializeField] float cost;
    [SerializeField] float damage;
    [SerializeField] float duration;
    [SerializeField] float deathTime;
    [SerializeField] bool isExplosive;
    [SerializeField] bool traversesUnits;

    public float DeathTime { get { return deathTime; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deathTime = (deathTime <= 0) ? 0 : deathTime - Time.deltaTime;
        duration = (duration <= 0) ? 0 : duration - Time.deltaTime;

        OnShoot();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Enemy") return;


    }

    public virtual void OnShoot() { }
}
