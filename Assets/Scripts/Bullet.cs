using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : PoolableObject
{
    public float speed;
    private Vector3 _direction;
    private float _curSpeed;

    private const float MAX_HORIZONTAL = 50;
    private const float MAX_VERTICAL = 50;

    private Rigidbody2D _rb;

    private bool _dead;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public override void Initialize()
    {
        //do nothing, for now
    }

    private void OnEnable()
    {
        _dead = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_dead) return;
        _rb.MovePosition(transform.position + transform.up * speed * Time.deltaTime);


        if (transform.position.magnitude > EnemySpawner.Radius)
        {
            print("out of bounds");
            Die();
            Stats.StopStreak(); // stop streak since bullet never hit anything
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            Die();
        }
    }

    private void Die()
    {
        _dead = true;
        _rb.velocity = Vector3.zero;
        _pooler.ReleaseToPool(this);
    }
}
