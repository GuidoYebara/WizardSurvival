using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] Image lifeBarPlayer;
    [SerializeField] Text lifeNumPlayer;

    [SerializeField] Image currentWaveBar;
    [SerializeField] Text currentWave;

    [SerializeField] Text currentFPS;

    float timePause;
    private void Awake()
    {
        EventManager.OnUpdatePlayerLifeUI += UpdatePlayerLife;
        EventManager.OnUpdateWaveUI += UpdateWave;
    }

    private void Update()
    {
        timePause -= Time.deltaTime;
        if (currentFPS != null && timePause <= 0)
        {
            currentFPS.text = (1000 / Time.deltaTime).ToString();
            timePause = 0.5f;
        }
    }

    void UpdatePlayerLife(float currentHealth, float maxHealth)
    {
        if (lifeBarPlayer != null) lifeBarPlayer.fillAmount = currentHealth / maxHealth;
        if (lifeNumPlayer != null) lifeNumPlayer.text = currentHealth.ToString();
    }

    void UpdateWave(int currentWave, int currentEnemys, int maxEnemys)
    {
        if (currentWaveBar != null) currentWaveBar.fillAmount = currentEnemys / maxEnemys;
        if (this.currentWave != null) this.currentWave.text = currentWave.ToString();
    }
}
