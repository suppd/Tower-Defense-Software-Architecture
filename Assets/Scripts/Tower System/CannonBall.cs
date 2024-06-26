
using UnityEngine;
/// <summary>
///this script is responsible for all three types of bullets (standard,aoe dmg,debuff) it shoots the one based on which type
/// currently it just checks which upgrade value is > 0 and then marks it as the corresponding type its not the best way of doing it i know but i honestly didnt know how else to handle it 
/// </summary>
public class CannonBall : MonoBehaviour
{
    public float Speed = 5f;
    public int Damage = 1;
    public int SlowAmount = 2;
    public float SlowDuration = 3;
    public float ExplosionRadius = 0f;
    public float DebuffRadius = 0f;
    [SerializeField]
    private GameObject impactEffect;
    private Transform target;
    //three methods for updating the values 
    //Not the greatest approach if I wanted to make the project more scalabe I would make a "projectile" super class or something and then let this class inherit from there 
    //by doing that I can make an enum list of bullet types and then based on bullet type I can change the corresponding value i want to change using only one method
    private Type cannonBallType;
    enum Type
    {
        NORMAL,
        AOE,
        DEBUFF
    }
    public void CheckCannonBallType()
    {
        if (ExplosionRadius > 0f)
        {
            cannonBallType = Type.AOE;
            //Debug.Log("type set to AOE");
        }
        else if (SlowDuration > 0f)
        {
            cannonBallType = Type.DEBUFF;
           // Debug.Log("type set to DEBUFF");
        }
        else if (ExplosionRadius == 0 || SlowDuration == 0)
        {
            cannonBallType = Type.NORMAL;
            //Debug.Log("type set to NORMAL");
        }
    }
    public void SetBulletDamage(int damageToSet)
    {
        Damage = damageToSet;
    }
    public void SetBulletSlowDuration(float durationToSet)
    {
        SlowDuration = durationToSet;
    }
    public void SetBulletExplosionRadius(float radiusToSet)
    {
        ExplosionRadius = radiusToSet;
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
        float distanceThisFrame = Speed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame)
        {
            ShootTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }
    void ShootTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 3f);
        switch (cannonBallType)
        {
            case Type.NORMAL:
                DamageEnemy(target);
                break;
            case Type.AOE:
                Debug.Log("exploding");
                Explode();
                break;
            case Type.DEBUFF:
                Debuff();
                break;
        }
        Destroy(gameObject);
    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("enemy"))
            {
                DamageEnemy(collider.transform);
            }
        }
    }
    void Debuff()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("enemy"))
            {
                Slow(collider.transform);
            }
        }
    }
    void DamageEnemy(Transform enemy)
    {
        Monster e = enemy.GetComponent<Monster>();
        if (e != null)
        {
            e.Damage(Damage);
        }
    }
    void Slow(Transform enemy)
    {
        Monster e = enemy.GetComponent<Monster>();
        if (e != null)
        {
            if (!e.IsSlowed)
            {
                e.SlowDuration = SlowDuration;
                e.GetSlowed(SlowAmount);
            }
        }
    }
}
