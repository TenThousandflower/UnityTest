using System;
using DG.Tweening;
using UnityEngine;


public class PetData : MonoBehaviour
{
    public Vector3 offset;

    private Animator _animator;
    private Vector2 LookAt;
    private PlayerController _playerController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _playerController = transform.parent.GetComponent<PlayerController>();
        _animator.SetFloat("Speed", 1f);
    }

    private void LateUpdate()
    {
        Vector2 lookAt = _playerController.lookAt;

        if (lookAt.x >= 0 && lookAt.x >= Math.Abs(lookAt.y))
        {
            offset = transform.parent.GetComponent<Rigidbody2D>().position - Vector2.right * 0.5f;
        }
        else if (lookAt.x <= 0 && Math.Abs(lookAt.x) >= Math.Abs(lookAt.y))
        {
            offset = transform.parent.GetComponent<Rigidbody2D>().position - Vector2.left * 0.5f;
        }
        else if (lookAt.y >= 0 && lookAt.y > Math.Abs(lookAt.x))
        {
            offset = transform.parent.GetComponent<Rigidbody2D>().position - Vector2.up * 0.5f;
        }
        else if (lookAt.y <= 0 && Math.Abs(lookAt.y) > Math.Abs(lookAt.x))
        {
            offset = transform.parent.GetComponent<Rigidbody2D>().position - Vector2.down * 0.8f;
        }
        
        transform.DOMove(offset, 1).SetAutoKill(true);

        _animator.SetFloat("LookX", lookAt.x);
        _animator.SetFloat("LookY", lookAt.y);
    }
}