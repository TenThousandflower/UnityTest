using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    private Rigidbody2D rigidbody2d;

    private float horizontal;

    private float vertical;

    public int maxHealth = 5;

    public int health
    {
        get { return currentHealth; }
    }

    private int currentHealth;
    private bool isInvincible;
    private float invincibleTimer;
    public float timeInvincible = 2.0f;
    private Animator _animator;
    private Vector2 lookDirection = new Vector2(1, 0);
    public GameObject projectilePrefab;
    public GameObject StarEffect;
    public RubyData RubyData;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        _animator.SetFloat("Look X", lookDirection.x);
        _animator.SetFloat("Look Y", lookDirection.y);
        _animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f,
                LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
           Slot slot = ScriptableObject.CreateInstance<Slot>();
           slot.name = "测试";
           slot.exp = 0;
           slot.level = 1;
           string slotName = "slot" + RubyData.slots.Count;
           AssetDatabase.CreateAsset(slot, "Assets/Data/"+ slotName +".asset");
           RubyData.slots.Add(slot);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            _animator.SetTrigger("Hit");

            if (isInvincible)
                return;
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        Instantiate(StarEffect, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float) maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject =
            Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        _animator.SetTrigger("Launch");
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}