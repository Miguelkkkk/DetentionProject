using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttack : EnemyAttack
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected Transform bulletSpawner;

    private float timer;
    [SerializeField] private Animator _animator;
    protected override void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < attackDistance)
            {
                timer += Time.deltaTime;
                if (timer > attackSpeed)
                {
                    _animator.SetTrigger("Attack");
                    Shoot();
                    timer = 0;
                }

            }
        }
    }

    protected void Shoot()
    {
        Instantiate(bullet, bulletSpawner.position, Quaternion.identity);
    }
}
