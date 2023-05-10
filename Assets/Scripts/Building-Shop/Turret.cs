using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private EnemyStats targetEnemy;
    private MovementEnemy targetEnemyMovement;
    private bool leftCannonUsed = false;

    [Header("General")]
    public float range = 15;

    [Header("Bullet (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Laser")]

    public int damageOverTime = 30;
    public float slow = 0.5f;

    public bool useLaser = false;
    public LineRenderer lineRederer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    
    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnspeed = 10f;    
    public Transform firePoint;

    [Header("Only Standart Turret")]
    
    public Transform leftCannonFirePoint;
    public Transform rightCannonFirePoint;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
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
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<EnemyStats>();
            targetEnemyMovement = nearestEnemy.GetComponent<MovementEnemy>();
        }
    }
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRederer.enabled)
                {
                    lineRederer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }

            }

            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser(); 
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }


    }
    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    
    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemyMovement.Slow(slow);

        if (!lineRederer.enabled)
        { 
            lineRederer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRederer.SetPosition(0, firePoint.position);
        lineRederer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

        
    }

    void Shoot()
    {
        //Standart Turret
        if(leftCannonFirePoint!=null && rightCannonFirePoint !=null)
        {
            if (!leftCannonUsed)
            {
                GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, leftCannonFirePoint.position, leftCannonFirePoint.rotation);
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                if (bullet != null)
                    bullet.Seek(target);
                leftCannonUsed = true;
            }
            else
            {
                GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, rightCannonFirePoint.position, leftCannonFirePoint.rotation);
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                if (bullet != null)
                    bullet.Seek(target);
                leftCannonUsed = false;
            }
        }

        if(firePoint!=null)
        {
            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            if (bullet != null)
                bullet.Seek(target);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color= Color.green;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
