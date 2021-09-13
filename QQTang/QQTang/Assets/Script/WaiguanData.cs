using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiguanData : MonoBehaviour
{
    public int type; //1 头，2 背，3 脚

    private Animator _animator;
    private Vector2 LookAt = new Vector2(0f, -1f);
    private PlayerController _playerController;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerController = transform.parent.parent.GetComponent<PlayerController>();

        if (type == 1)
        {
            _spriteRenderer.sortingOrder = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeLook(_playerController.lookAt);
        _animator.SetFloat("LookX", LookAt.x);
        _animator.SetFloat("LookY", LookAt.y);
        if (type == 2)
        {
            if (LookAt.y >= 0f)
            {
                _spriteRenderer.sortingOrder = 10;
            }
            else
            {
                _spriteRenderer.sortingOrder = 0;
            }
        }
    }


    private void ChangeLook(Vector2 newLook)
    {
        LookAt.Set(newLook.x, newLook.y);
    }

    public void ChangeParent(PlayerController playerController)
    {
        _playerController = playerController;
    }
}