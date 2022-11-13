using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;
    [SerializeField] float currentShield;
    [SerializeField] float maxShield;

    [SerializeField] bool isInvulnerable;

    public float Health { get { return currentHealth; } }
    public bool IsInvulnerable { get { return isInvulnerable; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) OnDeath();
    }

    private void OnDeath()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float dmg)
    {
        if (isInvulnerable) return;

        currentHealth = (currentHealth > 0) ? currentHealth - dmg : 0;
    }
    public void Revive(double healthPercent) // Ingrese Value 0.0 to 1.0
    {
        currentHealth = maxHealth * (float)healthPercent;
        gameObject.SetActive(true);
    }
    public void Revive(Vector3 position, double healthPercent)
    {
        currentHealth = maxHealth * (float)healthPercent;
        transform.position = position;
        gameObject.SetActive(true);
    }
}
