using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : PoolableObject
{
    
    public float speed;
    private Vector3 targetDirection;
    private Rigidbody2D _rb;
    private EnemySprite _sprite;
    private Collider2D _collider;
    private bool _dead;
    private AudioSource _deathSound;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<EnemySprite>();
        _collider = GetComponent<Collider2D>();
        _deathSound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _dead = false;
        _collider.enabled = true;
    }

    public override void Initialize()
    {
        targetDirection = (Vector3.zero - transform.position).normalized;
    }

    void FixedUpdate()
    {
        if (_dead) return;
        _rb.MovePosition(transform.position + speed * Time.deltaTime * targetDirection);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            // Die
            _dead = true;
            _collider.enabled = false;
            if (!GameManager.AudioMuted) _deathSound.Play();

            // Play Death Animation
            _sprite.PlayAnim();

            // Release bullet to pooler
            var bullet = collision.GetComponent<Bullet>();
            bullet.ReleaseToPool();

            // Increment Score
            Stats.ChangeScore(1);
            Stats.IncrementShotsLanded();
        }
    }

    
}
