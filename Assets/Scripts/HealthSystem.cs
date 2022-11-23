using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;
    [SerializeField] float currentShield;
    [SerializeField] float maxShield;
    [SerializeField] bool isInvulnerable;
    [SerializeField] bool isPlayer;

    [SerializeField] AudioSource _as;
    [SerializeField] string audioName;

    public float Health { get { return currentHealth; } }
    public bool IsInvulnerable { get { return isInvulnerable; } }

    private void Start()
    {
        _as = GetComponent<AudioSource>();
        UpdateUI();
    }

    void Update()
    {
        if (currentHealth <= 0) OnDeath();
    }

    private void OnDeath()
    {
        if (isPlayer)
        {
            EventManager.TriggerOnPlayerDeath();
        }
        else
        {
            EventManager.TriggerOnEnemyDeath(gameObject);
        }

        gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if(isPlayer)
            EventManager.UpdatePlayerLifeUI(currentHealth, maxHealth);
    }

    public void TakeDamage(float dmg, TypeOfSpellElement elementType) // Valores Negativos Suman Vida
    {
        if (isInvulnerable) return;
        EventManager.PlaySound(audioName, _as);
        currentHealth = (currentHealth > 0) ? currentHealth - dmg : 0;
        UpdateUI();
    }

    public void Revive(double healthPercent) // Ingrese Value 0.0 to 1.0
    {
        currentHealth = maxHealth * (float)healthPercent;
        
        UpdateUI();
        
        gameObject.SetActive(true);
    }
    public void Revive(Vector3 position, double healthPercent)
    {
        currentHealth = maxHealth * (float)healthPercent;
        transform.position = position;
        
        UpdateUI();
        
        gameObject.SetActive(true);
    }
    public void SwitchVulnerable(bool isInvulnerable) => this.isInvulnerable = isInvulnerable;
}
