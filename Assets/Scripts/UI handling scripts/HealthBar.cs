using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Player healtbar script for the UI
/// </summary>
public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Image fill;
    public Gradient gradient;
    public Zombie zombie;

    private float health;
    private void Awake()
    {
        health = zombie.health;
        healthBar.maxValue = health;
        fill.color = gradient.Evaluate(1f);
    }
    public void UpdateHealthBar()
    {
        health = zombie.health;
        healthBar.value = health;

        fill.color = gradient.Evaluate(healthBar.normalizedValue);
    }

    private void FixedUpdate()
    {
        UpdateHealthBar();
    }
}
