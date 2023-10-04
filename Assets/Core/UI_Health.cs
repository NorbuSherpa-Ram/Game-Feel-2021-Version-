using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Health : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] TextMeshProUGUI healthCount;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }
    private void OnEnable()
    {
        playerHealth.OnHurt += UpdatePlayerHealthUI;
    }
    private void OnDisable()
    {
        playerHealth.OnHurt -= UpdatePlayerHealthUI;
    }

    private void Start()
    {
        UpdatePlayerHealthUI();
    }




    private void UpdatePlayerHealthUI()
    {
        healthCount.text = playerHealth.GetCurrentHealthString();
    }
}
