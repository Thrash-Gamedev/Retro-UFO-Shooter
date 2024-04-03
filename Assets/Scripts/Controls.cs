using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public float turnSpeed;
    public Transform shootPoint;
    public ObjectPooler bulletPool;
    public float coolDownTime;

    private  AudioSource fireAudio;
    private bool readyToShoot;

    private void Awake()
    {
        readyToShoot = true;
        fireAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        // Turning
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.RotateAround(transform.position, Vector3.forward, turnSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.RightArrow))
            transform.RotateAround(transform.position, Vector3.forward, -turnSpeed * Time.deltaTime);


        // Shooting
        if (Input.GetKeyDown(KeyCode.Space) && readyToShoot)
        {

            bulletPool.SpawnAt(shootPoint.position, transform.rotation);
            Stats.IncrementShotsFired();

            LayerMask enemyMask = LayerMask.GetMask("enemy");
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.1f, transform.up, EnemySpawner.Radius, enemyMask);
/*            Debug.Log(hit.point);
            Debug.DrawLine(transform.position, transform.position + transform.up * EnemySpawner.Radius, Color.red, 0.5f);*/
       
            if (hit.collider == null)
            {
                Stats.StopStreak();
            }
            StartCoroutine(CoolDownRoutine());

            if(!GameManager.AudioMuted) fireAudio.Play();
        }
            
            
            
    }

    IEnumerator CoolDownRoutine()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(coolDownTime);
        readyToShoot = true;
    }
}
