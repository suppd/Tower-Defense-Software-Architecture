
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    // this script is responsible for all three types of bullets (standard,aoe dmg,debuff)

    //public variables for other classes 
    public float speed = 5f;
    public int damage = 1;
    public int slowAmount = 50;
    public float slowDuration = 3;
    public float explosionRadius = 0f;
    public float debuffRadius = 0f;

    [SerializeField]
    private GameObject impactEffect;

    private Transform target;

    //three methods for updating the values 
    public void setBulletDamage(int inputDamage)
    {
        damage = inputDamage;
    }
    public void setBulletSlowDuration(float inputDuration)
    {
        slowDuration = inputDuration;
    }
    public void setBulletExplosionRadius(float inputRadius)
    {
        explosionRadius = inputRadius;
    }
    public void FireAtTarget(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        if (debuffRadius > 0f)
        {
            Debuff();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "enemy")
            {
                Damage(collider.transform);
            }
        }
    }
    void Debuff()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "enemy")
            {
                Slow(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Monster e = enemy.GetComponent<Monster>();

        if (e != null)
        {
            e.TakeDamage(damage);
            
        }
    }

    void Slow(Transform enemy)
    {
        Monster e = enemy.GetComponent<Monster>();

        if (e != null)
        {
            if (!e.isSlowed)
            {
                e.slowDuration = slowDuration;
                Debug.Log("montser slowed!");
                e.GetSlowed(slowAmount);
            }
        }
    }
}
