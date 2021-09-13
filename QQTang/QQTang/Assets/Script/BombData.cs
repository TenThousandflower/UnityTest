using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BombData : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject endExplosionPrefab;
    private BoxCollider2D _boxCollider2D;

    public bool isBlast;
    public int bombPower;
    private float timeLimit = 2f;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", timeLimit);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            _boxCollider2D.isTrigger = false;
        }
    }

    private void OnDestroy()
    {
        PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerController.BombBlast();
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, quaternion.identity);
        BombBlast();
    }

    public void BombBlast()
    {
        if (!isBlast)
        {
            gameObject.layer = 0;
            CreateExplode(Vector2.left, Vector3.left);
            CreateExplode(Vector2.right, Vector3.right);
            CreateExplode(Vector2.up, Vector3.up);
            CreateExplode(Vector2.down, Vector3.down);
            isBlast = true;
            Destroy(gameObject, 0.3f);
        }
    }

    private void CreateExplode(Vector2 direction, Vector3 direction3D)
    {
        for (int i = 1; i < bombPower + 1; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), direction, i,
                LayerMask.GetMask("Qiang"));

            if (!hit.collider)
            {
                RaycastHit2D hitCheckBomb = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
                    direction, i,
                    LayerMask.GetMask("Bomb"));

                if (hitCheckBomb.transform)
                {
                    BombData bombData = hitCheckBomb.transform.GetComponent<BombData>();
                    bombData.BombBlast();
                }

                RaycastHit2D hitCheckPlayer = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
                    direction, i,
                    LayerMask.GetMask("Player"));

                if (hitCheckPlayer.transform)
                {
                    PlayerController playerController = hitCheckPlayer.transform.GetComponent<PlayerController>();
                    playerController.BlastPlayer();
                }


                if (i == bombPower)
                {
                    if (direction.x > 0)
                    {
                        Instantiate(endExplosionPrefab, transform.position + (i * direction3D),
                            Quaternion.Euler(new Vector3(0, 0, 90)));
                    }
                    else if (direction.x < 0)
                    {
                        Instantiate(endExplosionPrefab, transform.position + (i * direction3D),
                            Quaternion.Euler(new Vector3(0, 0, 270)));
                    }
                    else if (direction.y > 0)
                    {
                        Instantiate(endExplosionPrefab, transform.position + (i * direction3D),
                            Quaternion.Euler(new Vector3(0, 0, 180)));
                    }
                    else if (direction.y < 0)
                    {
                        Instantiate(endExplosionPrefab, transform.position + (i * direction3D),
                            Quaternion.Euler(new Vector3(0, 0, 360)));
                    }
                }
                else
                {
                    if (direction.x > 0)
                    {
                        Instantiate(explosionPrefab, transform.position + (i * direction3D),
                            Quaternion.Euler(new Vector3(0, 0, 90)));
                    }
                    else if (direction.x < 0)
                    {
                        Instantiate(explosionPrefab, transform.position + (i * direction3D),
                            Quaternion.Euler(new Vector3(0, 0, 270)));
                    }
                    else if (direction.y > 0)
                    {
                        Instantiate(explosionPrefab, transform.position + (i * direction3D),
                            Quaternion.Euler(new Vector3(0, 0, 180)));
                    }
                    else if (direction.y < 0)
                    {
                        Instantiate(explosionPrefab, transform.position + (i * direction3D),
                            Quaternion.Euler(new Vector3(0, 0, 360)));
                    }
                }
            }
            else
            {
                break;
            }
        }
    }
}