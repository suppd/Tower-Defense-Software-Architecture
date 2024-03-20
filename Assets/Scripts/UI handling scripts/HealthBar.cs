using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Player healtbar script for the UI
/// </summary>
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private Image fill;
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Monster monster;

    private float health;
    private void Awake()
    {
        health = monster.Health;
        healthBar.maxValue = health;
        fill.color = gradient.Evaluate(1f);
    }
    public void UpdateHealthBar()
    {
        health = monster.Health;
        healthBar.value = health;
        fill.color = gradient.Evaluate(healthBar.normalizedValue);
    }
}
