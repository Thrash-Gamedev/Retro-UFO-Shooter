using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip deathSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            GameManager.HandleGameOver();

            audioSource.clip = deathSound;
            if (!GameManager.AudioMuted) audioSource.Play();
        }
    }
}
