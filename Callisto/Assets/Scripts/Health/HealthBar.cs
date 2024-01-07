using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth; // Reference to player health controller
    [SerializeField] private Image totalHealthBar; // Refernence to player health bar when it is full
    [SerializeField] private Image currentHealthBar; // Refernence to player health bar currently filled in

    private void Start()
    {
        totalHealthBar.fillAmount = playerHealth.CurrentHealth / 10;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.CurrentHealth / 10;
    }
}
