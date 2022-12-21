using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{

    //each monster should input its own movementspeed and health
    
    public float MovementSpeed = 0f;
    public float health;
    public int worth = 0;
    public EnemyPathing pathingScript;

    private bool isDead = false;

    private void Awake()
    {
        pathingScript = GetComponent<EnemyPathing>();
    }
    public enum MonsterType
    {
        Zombie,
        Goblin
    }

    public abstract MonsterType GetMonsterType();

    public void TakeDamage(float amount)
    {
        health -= amount;

        //healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        MovementSpeed = MovementSpeed * (1f - pct);
    }

    void Die()
    {
        isDead = true;

        PlayerInfo.Money += worth;

       // GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
       // Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }

    void Update()
    {
        // put shared code between goblin and zombie here (if needed)
    }
}

