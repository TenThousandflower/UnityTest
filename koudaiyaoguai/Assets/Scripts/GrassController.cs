using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private Animator _animator;
    private float timer;

    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && timer <= 0)
        {
            _animator.SetTrigger("Dance");
            timer = 0.75f;
        }
    }
}