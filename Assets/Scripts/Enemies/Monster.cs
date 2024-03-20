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
    //all public vars that need to be accessed by other classes
    public float MovementSpeed = 0f;
    public float Health;
    public bool isSlowed = false;
    public float slowDuration = 3f;
    public EnemyPathing pathingScript; //public so it can be accessed in the wavespawner class
    [SerializeField]
    private HealthBar healthBarScript;
    [SerializeField]
    private int moneyValue = 0;
    [SerializeField]
    private GameObject deathEffect;
    [SerializeField]
    private Collider collider;
    private bool isDead = false;
    private float originalMovement;
    private void OnEnable() // subscribe to eventbus
    {
        GlobalBus.GlobalEventBus.Subscribe<BaseEnterEvent>(HandleEnemyEnter);
    }
    private void OnDestroy() //unsubscribe to eventbus
    {
        Debug.Log("unsubscribing from eventbus because im being destroyed :(");
        GlobalBus.GlobalEventBus.UnSubscribe<BaseEnterEvent>(HandleEnemyEnter);
    }
    private void Awake() // pass in the path to follow
    {  
        pathingScript = GetComponent<EnemyPathing>();
    }
    public enum MonsterType //could define more types here in the future like goblin mage etc
    {
        Skeleton,
        ArmoredSkeleton
    }
    private void Start()
    {
        originalMovement = MovementSpeed;
        healthBarScript.UpdateHealthBar(); // run once so that healthbar is properly intialized
    }
    public abstract MonsterType GetMonsterType();
    public void Damage(float amount)
    {  
        Health -= amount;
        healthBarScript.UpdateHealthBar();
        if (Health <= 0 && !isDead)
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
        MovementSpeed = originalMovement;
        isSlowed=false;
    }
    public void Die()
    {
        isDead = true;
        PlayerInfo.Instance.AddPlayerMoney(moneyValue); //update players money value
        //vfx for dying
        GameObject effect = (GameObject)Instantiate(deathEffect, new Vector3(transform.position.x,transform.position.y + 1,transform.position.z), Quaternion.identity);
        effect.GetComponent<SetMoneyDropText>().setMoneyTextValue(moneyValue);
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
            if (baseEnterEvent.BoxCollider == collider)
            {
                Die();
            }
        }
    }
    void Update()
    {
        // put shared code between monster types here (if needed)
    }
}

