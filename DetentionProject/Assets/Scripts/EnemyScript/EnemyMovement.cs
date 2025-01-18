using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMovement : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Transform target;
    protected Animator animator;
    [SerializeField] protected float chaseRadius = 10f;
    protected bool isTargetDefined;
    protected new Rigidbody2D rigidbody2D;

    protected void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
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

                Vector3 agentVelocity = agent.velocity;
                Vector3 movementDirection = agentVelocity.normalized;

                animator.SetFloat("Horizontal", movementDirection.x);
                animator.SetFloat("Vertical", movementDirection.y);

                if (movementDirection.x != 0)
                {
                    Vector3 scale = transform.localScale;
                    scale.x = Mathf.Abs(scale.x) * (movementDirection.x > 0 ? 1 : -1);
                    transform.localScale = scale;
                }
            }
            else
            {
                agent.ResetPath();
                isTargetDefined = false;
                animator.SetBool("isMoving", false);

                animator.SetFloat("Horizontal", 0f);
                animator.SetFloat("Vertical", 0f);
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
