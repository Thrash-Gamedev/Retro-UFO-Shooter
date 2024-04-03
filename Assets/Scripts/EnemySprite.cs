using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySprite : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Sprite _originalSprite;
    private PoolableObject enemy;
    private Animator _anim;

    private void Awake()
    {
        enemy = transform.parent.GetComponent<PoolableObject>();
        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _originalSprite = _renderer.sprite;
    }

    public void PlayAnim()
    {
        _anim.SetTrigger("Die");
    }

    public void Release()
    {
        _renderer.sprite = _originalSprite;
        enemy.ReleaseToPool();
    }
}
