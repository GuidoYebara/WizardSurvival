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

    [SerializeField] Image lifeBar;
    [SerializeField] Text lifeNumber;

    public float Health { get { return currentHealth; } }
    public bool IsInvulnerable { get { return isInvulnerable; } }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) OnDeath();
    }

    private void OnDeath()
    {
        gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if (lifeBar != null) lifeBar.fillAmount = currentHealth / maxHealth;
        if (lifeNumber != null) lifeNumber.text = currentHealth.ToString();
    }

    public void TakeDamage(float dmg) // Valores Negativos Suman Vida
    {
        if (isInvulnerable) return;

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
    public void SwitchVulnerable(bool isInvulerable) => this.isInvulnerable = isInvulerable;
}
