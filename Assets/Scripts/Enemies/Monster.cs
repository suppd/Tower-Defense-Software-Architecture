using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    /// <summary>
    /// this is the abstract class for all monsters so all enemies are based off this class
    /// meaning they all have a movementspeed variable health varialbe a money worth variable and some other variables
    /// also jsut handles general stuff enemies need to have like take damage method get slowed method and die methods etc
    /// </summary>
    //each monster should input its own movementspeed and health
    public float MovementSpeed = 0f;
    public float health;
    public int worth = 0;
    public bool isSlowed = false;
    public float slowDuration = 3f;
    public EnemyPathing pathingScript; //public so it can be accessed in the wavespawner class
    public HealthBar healthBarScript;
    [SerializeField]
    private GameObject deathEffect;
    [SerializeField]
    private Collider collider;
    private bool isDead = false;
    private float OriginalMovement;
    private void OnEnable() // subscribe to eventbus
    {
        GlobalBus.globalEventBus.Subscribe<BaseEnterEvent>(HandleEnemyEnter);
    }
    private void OnDestroy() //unsubscribe to eventbus
    {
        Debug.Log("unsubscribing from eventbus because im being destroyed :(");
        GlobalBus.globalEventBus.UnSubscribe<BaseEnterEvent>(HandleEnemyEnter);
    }
    private void Awake() // pass in the path to follow
    {  
        pathingScript = GetComponent<EnemyPathing>();
    }
    public enum MonsterType //could define more types here in the future like goblin mage etc
    {
        Skeleton
    }
    private void Start()
    {
        OriginalMovement = MovementSpeed;
        healthBarScript.UpdateHealthBar(); // run once so that healthbar is properly intialized
    }
    public abstract MonsterType GetMonsterType();
    public void TakeDamage(float amount)
    {  
        health -= amount;
        healthBarScript.UpdateHealthBar();
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }
    public void GetSlowed(float amount)
    {
        StartCoroutine(Slowtimer());
        Slow(amount);
    }
    public void Slow(float pct)
    {
        isSlowed = true;
        MovementSpeed = MovementSpeed - pct;
    }
    IEnumerator Slowtimer()
    {
        yield return new WaitForSeconds(slowDuration);
        MovementSpeed = OriginalMovement;
        isSlowed=false;
    }
    public void Die()
    {
        isDead = true;
        PlayerInfo.Instance.AddPlayerMoney(worth); //update players money value
        //vfx for dying
        GameObject effect = (GameObject)Instantiate(deathEffect, new Vector3(transform.position.x,transform.position.y + 1,transform.position.z), Quaternion.identity);
        effect.GetComponent<SetMoneyDropText>().setMoneyTextValue(worth);
        Destroy(effect, 1f);
        //update enemies alive on wavemanagement system
        WaveSpawner.EnemiesAlive--;
        //then finally destroy the monster object
        Destroy(gameObject);
    }
    private void HandleEnemyEnter(object sender,EventArgs eventArgs)
    {
        BaseEnterEvent baseEnterEvent = (BaseEnterEvent)eventArgs;
        if (baseEnterEvent != null && collider != null)
        {
            if (baseEnterEvent.boxCollider == collider)
            {
                Die();
            }
        }
    }
    void Update()
    {
        // put shared code between goblin and zombie here (if needed)
    }
}

