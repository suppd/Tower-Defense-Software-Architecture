using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooting : MonoBehaviour
{
    [Header("Turret Variables")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float range = 0.0002f;

    
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    

    private Transform target;
    bool TargetInRange = false;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
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
        {
            bullet.FireAtTarget(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
