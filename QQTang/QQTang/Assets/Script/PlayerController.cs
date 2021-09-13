using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int bombNum; //炸弹数量
    public int bombPower; //炸弹威力
    public float maxSpeed;
    public int maxBombNum;
    public int maxBombPower;

    public bool isBlast; //被炸

    public Vector2 lookAt = new Vector2(0f, -1f);

    private GameObject bombPrefab;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private float walkx;
    private float walky;
    private bool canMove = true;
    private bool isInvincible = true; //无敌状态


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        bombPrefab = (GameObject) Resources.Load("Prefab/Bomb/b1");
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            walkx = Input.GetAxis("Horizontal");
            walky = Input.GetAxis("Vertical");
            Vector2 move = new Vector2(walkx, walky);
            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                lookAt.Set(move.x, move.y);
                lookAt.Normalize();
            }

            _animator.SetFloat("LookX", lookAt.x);
            _animator.SetFloat("LookY", lookAt.y);
            _animator.SetFloat("Speed", move.magnitude);
        }


        if (Input.GetKeyDown(KeyCode.Space) && bombNum > 0)
        {
            bombNum -= 1;
            CreateABomb();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector2 position = _rigidbody2D.position;
            position.x = position.x + speed * Time.deltaTime * walkx;
            position.y = position.y + speed * Time.deltaTime * walky;
            _rigidbody2D.position = position;
        }
    }

    public void BombBlast()
    {
        bombNum += 1;
    }

    public void ChooseBomb(GameObject bomb)
    {
        bombPrefab = bomb;
    }

    private void CreateABomb()
    {
        GameObject bomb = Instantiate(bombPrefab,
            new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y),
                transform.position.z), Quaternion.identity);
        BombData bombData = bomb.GetComponent<BombData>();
        bombData.bombPower = bombPower;
        bomb.GetComponent<Renderer>().sortingOrder = -1;
    }

    public void BlastPlayer()
    {
        if (!isBlast && !isInvincible)
        {
            isBlast = true;
            canMove = false;
            transform.Find("Tou").gameObject.SetActive(false);
            transform.Find("Bei").gameObject.SetActive(false);
            transform.Find("Jiao").gameObject.SetActive(false);
            _animator.SetBool("IsBlast", isBlast);
            StartCoroutine(PlayerLoseNatural());
        }
    }

    private IEnumerator PlayerLoseNatural()
    {
        yield return new WaitForSeconds(3f);
        if (isBlast)
        {
            PlayerLose();
        }
    }

    private void PlayerLose()
    {
        _animator.SetBool("Lose", true);
    }

    public void IntoGameScene()
    {
        // canMove = false;
        isInvincible = false;
        _rigidbody2D.position = new Vector2(0, 0);
        lookAt = new Vector2(0f, -1f);
    }
}