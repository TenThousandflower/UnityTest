using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public float hp = 10;
    public GameObject blastEffectPrefab;
    public Slider _slider;
    private Transform[] positions;
    private int index = 0;

    private float maxHp;

    // Start is called before the first frame update
    void Start()
    {
        positions = WayPoints.positions;
        maxHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (index < positions.Length)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index += 1;
        }

        if (index > positions.Length - 1)
        {
            MoveToEnd();
        }
    }

    private void MoveToEnd()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }

    public void TackDamage(float damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        _slider.value = hp / maxHp;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameObject effect = Instantiate(blastEffectPrefab, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }
}