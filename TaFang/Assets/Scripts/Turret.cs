using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();
    public float attackRateTime = 1f;
    public GameObject bulletPrefab;
    public Transform firePosition;
    public bool isLaser;
    public LineRenderer laser;
    public float damage;
    public GameObject ExplosionEffect;
    private float timer = 0f;
    private Transform turretHead;

    private void Awake()
    {
        turretHead = transform.Find("Head");
    }

    private void Start()
    {
        timer = attackRateTime;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (!isLaser)
        {
            if (enemys.Count > 0 && timer >= attackRateTime)
            {
                timer = 0;
                Attack();
            }
        }
        else
        {
            if (enemys.Count > 0)
            {
                Laser();
            }
            else
            {
                DestroyLaser();
            }
        }


        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = turretHead.position.y;
            turretHead.LookAt(targetPosition);
        }
    }

    private void Attack()
    {
        RemoveNullEnemy();
        
        if (enemys.Count > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
    }

    private void Laser()
    {
        if (enemys[0] == null)
        {
            RemoveNullEnemy();
        }

        if (enemys.Count > 0)
        {
            laser.gameObject.SetActive(true);
            ExplosionEffect.SetActive(true);
            laser.SetPositions(new Vector3[] {firePosition.position, enemys[0].transform.position});
            ExplosionEffect.transform.position = enemys[0].transform.position;
            ExplosionEffect.transform.LookAt(transform.position);
            if (timer >= attackRateTime)
            {
                enemys[0].GetComponent<Enemy>().TackDamage(damage);
                timer = 0;
            }
        }
    }

    private void DestroyLaser()
    {
        if (laser.gameObject.activeSelf)
        {
            laser.gameObject.SetActive(false);
            ExplosionEffect.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Remove(other.gameObject);
        }
    }

    public void RemoveNullEnemy()
    {
        List<int> emptyIndex = new List<int>();
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i] == null)
            {
                emptyIndex.Add(i);
            }
        }

        if (emptyIndex.Count > 0)
        {
            for (int i = 0; i < emptyIndex.Count; i++)
            {
                enemys.RemoveAt(emptyIndex[i] - i);
            }
        }
    }
}