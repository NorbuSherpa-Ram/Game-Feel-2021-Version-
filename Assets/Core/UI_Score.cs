using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Score : MonoBehaviour
{
    public static Action OnMegaKill;

    private int currentkillCount = 0;
    private int previousKillCount;
    [Header("Mega Kill Info")]
    [SerializeField] private int megaKillCount = 10;
    [SerializeField] private float megaKillCoolDown = 5;
    private float lastKillTime;

    [SerializeField] private TextMeshProUGUI scoreText;
    private void OnEnable()
    {
        EnemyHealth.OnEnemyDeath += UpdateScoreUI;
    }
    private void OnDisable()
    {
        EnemyHealth.OnEnemyDeath -= UpdateScoreUI;
    }
    private void Start()
    {
        scoreText.text = currentkillCount.ToString("D3");
    }
    private void Update()
    {
        if(lastKillTime > Time.time)
        {
            if (megaKillCount < Mathf.Abs(currentkillCount - previousKillCount))
            {
                previousKillCount = currentkillCount;
                OnMegaKill?.Invoke();
                lastKillTime = 0;
            }
        }else
        {
            lastKillTime = 0;
            previousKillCount = currentkillCount;
        }
    }
    void UpdateScoreUI(int _scorePoint)
    {
        lastKillTime = Time.time+megaKillCoolDown;
        currentkillCount += _scorePoint;
        scoreText.text = currentkillCount.ToString("D3");
    }
}
