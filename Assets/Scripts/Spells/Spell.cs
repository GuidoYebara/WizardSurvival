using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] float cost;
    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] float duration;
    [SerializeField] float deathTime;

    [SerializeField] bool isAutoAim;
    [SerializeField] bool isExplosive;
    [SerializeField] bool traversesUnits;

    [SerializeField] string description;

    [SerializeField] GameObject targetPos;

    [SerializeField] Sprite icon;
    [SerializeField] SpellSO reference;
    [SerializeField] Rigidbody rb;

    public float Cost { get { return cost; } }
    public float Speed { get { return speed; } }
    public float Damage { get { return damage; } }
    public float DeathTime { get { return deathTime; } }



    // Start is called before the first frame update
    void Start()
    {
        Init();
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        deathTime = (deathTime <= 0) ? 0 : deathTime - Time.deltaTime;
        duration = (duration <= 0) ? 0 : duration - Time.deltaTime;

        if (duration <= 0) Desactivate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision) // [NEW]
    {
        if (collision.transform.tag != "Enemy") return;

        float dmg = Dmg();

        if (!traversesUnits) Desactivate();
    }

    private void Init()
    {
        cost = reference.cost;
        speed = reference.speed;
        damage = reference.damage;
        duration = reference.duration;

        isAutoAim = reference.isAutoAim;
        isExplosive = reference.isExplosive;
        traversesUnits = reference.traversesUnits;

        description = reference.description;
    } // [NEW]
    private void Desactivate()
    {
        gameObject.SetActive(false);
    } // [NEW]
    private float Dmg()
    {
        return damage;
    } // [NEW]
    private void Move()
    {
        if (isAutoAim)
        {
            Vector3 toPosition = targetPos.transform.position - transform.position;
            toPosition.Normalize();
            rb.MovePosition(transform.position + toPosition * speed * Time.fixedDeltaTime);
            return;
        }

        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    } // [NEW]

    public void Restart(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;

        deathTime = reference.deathTime;

        gameObject.SetActive(true);
    } // [NEW]
    public string Description()
    {
        string descPlus = description + 
            $"\nCost: {cost: 0.0}" +
            $"\nSpeed: {speed: 0.0}" +
            $"\nDamage: {damage: 0.0}" +
            $"\nDeathTime: {deathTime: 0.0}";

        return descPlus;
    } // [NEW]
}
