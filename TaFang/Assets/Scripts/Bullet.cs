using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 50f;
    public float speed = 20f;
    public GameObject explosionEffectPrefab;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Die();
            return;
        }

        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Vector3 dir = target.position - transform.position;
        // if (dir.magnitude < 1)
        // {
        //     target.GetComponent<Enemy>().TackDamage(damage);
        //     Die();
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TackDamage(damage);
            Die();
        }
    }

    private void Die()
    {
        GameObject effect = Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }
}