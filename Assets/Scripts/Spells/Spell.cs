using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public enum TypeOfSpellElement
{
    None,
    Fire,
    Thunder,
    Ice
}

public enum TypeOfSpells
{
    Fireball,
    Ligthingball
}

public class Spell : MonoBehaviour
{
    [SerializeField] protected TypeOfSpells spellType;

    [SerializeField] protected float cost;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected float duration;
    [SerializeField] protected float deathTime;

    [SerializeField] protected bool isAutoAim;
    [SerializeField] protected bool isExplosive;
    [SerializeField] protected bool traversesUnits;

    [SerializeField] protected string description;

    [SerializeField] protected GameObject targetPos;

    [SerializeField] protected Sprite icon;
    [SerializeField] protected SpellSO reference;
    [SerializeField] protected Rigidbody rb;

    [SerializeField] protected TypeOfSpellElement spellTypeElement;

    public delegate void _OnDeath(GameObject go);
    public event _OnDeath OnDeath;

    public TypeOfSpells SpellType { get { return spellType; } }
    public float Cost { get { return cost; } }
    public float Speed { get { return speed; } }
    public float Damage { get { return damage; } }
    public float DeathTime { get { return deathTime; } }

    void Start()
    {
        if(reference) Init();
        rb = transform.GetComponent<Rigidbody>();
    }

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

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.GetComponent<HealthSystem>()) return;
        if (collision.transform.tag != "Enemy") return;

        collision.transform.GetComponent<HealthSystem>().TakeDamage(Dmg());
        
        
        
        if (!traversesUnits) Desactivate();
    }

    private void Init()
    {
        spellType = reference.spellType;

        cost = reference.cost;
        speed = reference.speed;
        damage = reference.damage;
        duration = reference.duration;

        isAutoAim = reference.isAutoAim;
        isExplosive = reference.isExplosive;
        traversesUnits = reference.traversesUnits;

        description = reference.description;

        icon = reference.icon;

        spellTypeElement = reference.spellTypeElement;
    }
    private void Desactivate()
    {
        OnDeath(gameObject);
        gameObject.SetActive(false);
    }

    protected virtual void Move()
    {
        if (isAutoAim)
        {
            Vector3 toPosition = targetPos.transform.position - transform.position;
            toPosition.Normalize();
            rb.MovePosition(transform.position + toPosition * speed * Time.fixedDeltaTime);
            return;
        }

        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }
    protected virtual float Dmg()
    {
        return damage;
    }

    public void Reset(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;

        duration = reference.duration;

        gameObject.SetActive(true);
    }
    public string Description()
    {
        string descPlus = 
            $"Cost: {cost: 0.0}" +
            $"\nSpeed: {speed: 0.0}" +
            $"\nDamage: {damage: 0.0}" +
            $"\nDeathTime: {deathTime: 0.0}\n\n"
            + description;

        return descPlus;
    }
}
