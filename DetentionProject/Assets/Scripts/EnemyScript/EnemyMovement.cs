using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMovement : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Transform target;
    protected Animator animator;
    [SerializeField] private float chaseRadius = 10f;
    protected bool isTargetDefined;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected void Update()
    {
        DetectPlayer();

        if (isTargetDefined)
        {
            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                                              new Vector2(target.position.x, target.position.y));

            if (distance <= chaseRadius)
            {
                agent.SetDestination(target.position);
                animator.SetBool("isMoving", true);
            }
            else
            {
                agent.ResetPath();
                isTargetDefined = false;
                animator.SetBool("isMoving", false);
            }
        }
    }

    private void DetectPlayer()
    {
        if (!isTargetDefined)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
                isTargetDefined = true;
            }
        }
    }
}
