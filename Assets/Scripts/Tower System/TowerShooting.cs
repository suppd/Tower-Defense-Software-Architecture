using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooting : MonoBehaviour
{
    // this script is just responsble for making it so that every tower prefab can shoot and hit an enemy
    public TowerInfo towerInfo;
    [Header("Turret Variables")]

    [SerializeField]
    private GameObject bulletPrefab; // cannonball prefab (any of the 3 diffrent ones can be put in)

    [SerializeField]
    private Transform bulletSpawn;

    [SerializeField]
    private float range = 2f;

    //local variables
    private Transform target;
    private float fireCountdown = 0f;
    private bool TargetInRange = false;

    //upgradable values
    public int fireRate;
    public int bulletDamage;
    public float slowDuration;
    public float explosionRadius;
    private void Awake()
    {
        //towerInfo = GetComponentInParent<TowerInfo>();
    }
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
        fireRate = towerInfo.towerFireRate;
        bulletDamage = towerInfo.towerDamage;
        slowDuration = towerInfo.towerSlowDuration;
        explosionRadius = towerInfo.towerExplosionRadius;
    }

    private void Update()
    {
        if (target == null)
        {
            this.transform.eulerAngles.Set(0, 210, 0);
            return;
        }
        if (fireCountdown < 0f)
        {
            fireCountdown = 1f / fireRate;
            Shoot();
            Debug.Log("shot bullet");
        }
        fireCountdown -= Time.deltaTime;

        if (TargetInRange == true)
        {
       
        }
    }

    void UpdateTarget()
    {
        //Debug.Log("looking for target");
        //Debug.Log(target);
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

    void Shoot()
    {
        GameObject Bullet_ = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        CannonBall bullet = Bullet_.GetComponent<CannonBall>();

        if (bullet != null)
        { // here it gives the bullet that it spawns the damage values that the tower has (because the bullet is actually what makes damage come to  the enemy)
            bullet.setBulletDamage(bulletDamage);
            bullet.setBulletSlowDuration(slowDuration);
            bullet.setBulletExplosionRadius(explosionRadius);
            bullet.FireAtTarget(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
