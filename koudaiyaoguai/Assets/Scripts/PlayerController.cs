using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;

    public bool canMove = true;

    public Vector2 rigidbody2D
    {
        get { return _rigidbody2D.position; }
    }

    private Rigidbody2D _rigidbody2D;
    private float horizontal; //左右

    private float vertical; //上下
    private Vector2 lookDirection;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            Vector2 move = new Vector2(horizontal, vertical);
            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize(); //把向量长度缩放到1
            }

            _animator.SetFloat("LookX", lookDirection.x);
            _animator.SetFloat("LookY", lookDirection.y);
            _animator.SetFloat("Speed", move.magnitude); //move向量长度 
        }
        else
        {
            _animator.SetFloat("Speed", 0f);
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector2 position = _rigidbody2D.position;
            position.x = position.x + speed * horizontal * Time.deltaTime;
            position.y = position.y + speed * vertical * Time.deltaTime;
            _rigidbody2D.position = position;
        }
    }
}