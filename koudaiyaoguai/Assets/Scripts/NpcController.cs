using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class NpcController : MonoBehaviour
{
    public float timeLimit = 0.1f;
    public float lookDistance = 6f;
    public PlayerData PlayerData;
    private float time = 0.0f;

    private int[] x = new int[] {-1, 0, 1, 0};
    private int[] y = new int[] {0, -1, 0, 1};

    private int lookX = 0;
    private int lookY = 0;
    private Vector2 lookDirection = new Vector2(1, 0);
    private bool watch;
    private Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lookDirection.Set(x[lookX], y[lookY]);
        lookDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (!watch)
        {
            if (time < 0)
            {
                if (lookX < x.Length - 1 && !watch)
                {
                    lookX += 1;
                    lookY += 1;
                }
                else
                {
                    lookX = 0;
                    lookY = 0;
                }

                time = timeLimit;
                lookDirection.Set(x[lookX], y[lookY]);
                lookDirection.Normalize();
                _animator.SetFloat("LookX", lookDirection.x);
                _animator.SetFloat("LookY", lookDirection.y);
            }

            RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection, lookDistance,
                LayerMask.GetMask("Player"));
            if (hit.collider != null)
            {
                watch = true;
                StartCoroutine(WacthPlayer(hit));
            }
        }
    }

    private IEnumerator WacthPlayer(RaycastHit2D hit)
    {
        PlayerController controller = hit.collider.GetComponent<PlayerController>();
        transform.Find("gantan").gameObject.SetActive(true);
        controller.canMove = false;
        yield return new WaitForSeconds(1f);
        transform.Find("gantan").gameObject.SetActive(false);
        StartCoroutine(MoveToPlayer(controller));
    }

    private IEnumerator MoveToPlayer(PlayerController controller)
    {
        Vector2 newPos = new Vector2(controller.rigidbody2D.x - x[lookX] * 0.75f,
            controller.rigidbody2D.y - y[lookY] * 0.7f);
        Tweener tweener = transform.DOMove(newPos, 2).SetAutoKill(true);
        yield return tweener.WaitForCompletion();
        StartCoroutine(SpeekUI.instance.Speek("你好" + PlayerData.name + "，初次出外探险需要选择一个小精灵当同伴。\n那么请从下面三个精灵里面选一个作为你的初始精灵吧。" ));
    }
}