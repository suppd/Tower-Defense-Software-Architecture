using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooting : MonoBehaviour, ITowerObserver
{
    // this script is just responsble for making it so that every tower prefab can shoot and hit an enemy
    //NOTE : this script is attached to the actual cannon in the tower prefab not on the tower itself
    [Header("Turret Refrences")]
    [SerializeField]
    private TowerInfo towerInfo;
    [SerializeField]
    private GameObject bulletPrefab; // cannonball prefab (any of the 3 diffrent ones can be put in)
    [SerializeField]
    private Transform bulletSpawn;
    [Header("Turret Variables")]
    [SerializeField]
    private float range = 2f;
    [SerializeField]
    private float defaultTowerAngle = 210;
    //local variables
    private Transform target;
    private float fireCountdown = 0f;
    //upgradable values
    public int FireRate;
    public int BulletDamage;
    public float SlowDuration;
    public float ExplosionRadius;
    public void NotifyNormalTowerUpgrade(int newLevel, float newFireRate, int newDamage)
    {
        BulletDamage = newDamage;
    }
    public void NotifyAOETowerUpgrade(int newLevel, float newFireRate, float newRadius)
    {
        ExplosionRadius = newRadius;
    }
    public void NotifyDebuffTowerUpgrade(int newLevel, float newFireRate, float newDebuffDuration)
    {
        SlowDuration = newDebuffDuration;
    }
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
    }
    private void Update()
    {
        if (target == null)
        {
            this.transform.eulerAngles.Set(0, defaultTowerAngle, 0);
            return;
        }
        if (fireCountdown < 0f)
        {
            fireCountdown = 1f / FireRate;
            Shoot();
        }
        fireCountdown -= Time.deltaTime;
    }
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if ( nearestEnemy != null & shortestDistance <= range)
        {  
            target = nearestEnemy.transform;
            transform.LookAt(target);           
        }
        else
        {
            target = null;
        }
    }
    private void Shoot()
    {
        GameObject cannonBallPrefab = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        CannonBall cannonBall = cannonBallPrefab.GetComponent<CannonBall>();
        if (cannonBall != null)
        { // here it gives the bullet that it spawns the damage values that the tower has (because the bullet is actually what damages the enemy)
            cannonBall.setBulletDamage(BulletDamage);
            cannonBall.setBulletSlowDuration(SlowDuration);
            cannonBall.setBulletExplosionRadius(ExplosionRadius);
            cannonBall.CheckCannonBallType();
            cannonBall.FireAtTarget(target);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
