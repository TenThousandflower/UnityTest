using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    public float speed = 3.0f;

    public bool vertical;

    public float changeTime = 3.0f;

    private float timer;

    private int direction = 1;
    private Animator _animator;
    private bool broken = true;
    public ParticleSystem smokeEffect;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
            return;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    private void FixedUpdate()
    {
        if (!broken)
            return;

        Vector2 position = _rigidbody2D.position;
        if (vertical)
        {
            _animator.SetFloat("Move X", direction);
            _animator.SetFloat("Move Y", 0);
            position.x = position.x + speed * Time.deltaTime * direction;
        }
        else
        {
            _animator.SetFloat("Move X", 0);
            _animator.SetFloat("Move Y", direction);
            position.y = position.y + speed * Time.deltaTime * direction;
        }


        _rigidbody2D.position = position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        _rigidbody2D.simulated = false;
        _animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }
}