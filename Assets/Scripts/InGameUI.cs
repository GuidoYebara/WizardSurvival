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

    [SerializeField] Text score;
    [SerializeField] Text currentFPS;

    float timePause;
    int scoreAcum;

    public int Score { get { return scoreAcum; } }

    private void Awake()
    {
        EventManager.OnUpdatePlayerLifeUI += UpdatePlayerLife;
        EventManager.OnUpdateWaveUI += UpdateWave;
        EventManager.OnUpdateScoreUI += UpdateScore;
    }
    private void OnDisable()
    {
        EventManager.OnUpdatePlayerLifeUI -= UpdatePlayerLife;
        EventManager.OnUpdateWaveUI -= UpdateWave;
        EventManager.OnUpdateScoreUI -= UpdateScore;
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
    void UpdateWave(string currentWave, int currentEnemys, int maxEnemys)
    {
        Debug.Log(string.Format($"currentWave {0}, currentEnemys {1}, maxEnemys {2}", currentWave, currentEnemys, maxEnemys));
        if (currentWaveBar != null) currentWaveBar.fillAmount = currentEnemys / maxEnemys;
        if (this.currentWave != null) this.currentWave.text = currentWave;
    }
    void UpdateScore(int score)
    {
        scoreAcum += score;
        this.score.text = scoreAcum.ToString();
    }
}
