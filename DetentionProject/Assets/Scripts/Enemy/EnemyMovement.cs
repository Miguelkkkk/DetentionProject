using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private float speed;

    [SerializeField] private bool isInSlimeRange;

    [SerializeField] private bool isPlayerSpotted;

    [SerializeField] public Transform target;

    private Rigidbody2D _enemyRigidBody;

    private Animator _enemyAnimator;


    private void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
        _enemyRigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        isInSlimeRange = GetComponentInChildren<Interactor>().isInRange;
        if (isInSlimeRange)
        {
            isPlayerSpotted = true;
            
        }

        if (isPlayerSpotted)
        {
            Move();
        }
        else { 
            StopMove();
        }
    }
    private void Move()
    {
        Vector2 targetPos = this.target.position;
        Vector2 currentPos = this.transform.position;
       
        Vector2 direction = targetPos - currentPos;
        direction = direction.normalized;

        _enemyRigidBody.velocity = (this.speed * direction);

        _enemyAnimator.SetBool("IsMoving", true);
        
    }
    private void StopMove()
    {
        _enemyRigidBody.velocity = Vector2.zero;
        _enemyAnimator.SetBool("IsMoving", false);
    }

}
