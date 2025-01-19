using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBehavior : EnemyMovement
{
    [SerializeField] private GameObject explosionCollider; 
    [SerializeField] private float explosionRadius = 1.5f; 
    [SerializeField] private float explosionDelay = 1f;

    private bool isExploding = false;
    protected new void Update()
    {
        DetectPlayer();

        if (isTargetDefined && !isExploding)
        {
            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                                              new Vector2(target.position.x, target.position.y));

            if (distance <= explosionRadius)
            {
                StartCoroutine(Explode());
            }
            else if (distance <= chaseRadius)
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
                    scale.x = Mathf.Abs(scale.x) * (movementDirection.x > 0 ? -1 : 1); 
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

    private IEnumerator Explode()
    {
        isExploding = true;
        agent.ResetPath();
        animator.SetBool("isMoving", false);
        animator.SetTrigger("Death");
        rigidbody2D.bodyType = RigidbodyType2D.Static;

        yield return new WaitForSeconds(explosionDelay);
        if (explosionCollider != null)
        {
            explosionCollider.SetActive(true);
        }

        yield return new WaitForSeconds(explosionDelay);

        Destroy(gameObject, 0.2f); 
    }
}
