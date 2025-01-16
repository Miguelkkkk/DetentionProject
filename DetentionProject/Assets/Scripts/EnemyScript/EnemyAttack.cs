using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    protected GameObject player;
    protected Animator animator;
    protected float distance;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackDistance;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("player"); 
    }

    protected virtual void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > attackDistance) {
            animator.SetTrigger("Attack");
        }
    }
}
