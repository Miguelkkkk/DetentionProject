using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private float minDistance;

    [SerializeField]
    private float viewRadius;

    [SerializeField]
    private Rigidbody2D rigidbody;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private LayerMask viewLayer;

    [SerializeField]
    private Transform target;

    void Update()
    {
        //Search();
        if(target != null)
        {
            Move();
        }
        else
        {
            StopMoving();
        }


    }
    private void Move()
    {
        Vector2 targetPos = this.target.position;
        Vector2 currentPos = this.transform.position;
        float distance = Vector2.Distance(currentPos, targetPos);

        if (distance >= this.minDistance)
        {
            Vector2 direction = targetPos - currentPos;
            direction = direction.normalized;

            this.rigidbody.velocity = (this.speed * direction);

            this.animator.SetBool("IsMoving", true);
        }
        else
        {
            StopMoving();
        }
    }
    private void StopMoving()
    {
        this.rigidbody.velocity = Vector2.zero;
        this.animator.SetBool("IsMoving", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, this.viewRadius);
            
        if(target != null)
        {
            Gizmos.DrawLine(this.transform.position, this.target.position);
        }
    }
    private void Search()
    {
        Collider2D colider = Physics2D.OverlapCircle(this.transform.position, this.viewRadius);
        if (colider != null)
        {
            Vector2 currentPos = this.transform.position;
            Vector2 targetPos = colider.transform.position;
            Vector2 direction = targetPos - currentPos;
            direction = direction.normalized;
            RaycastHit2D hit = Physics2D.Raycast(currentPos, direction);
            if(hit.transform != null)
            {
                if (hit.transform.CompareTag("Player")) 
                {
                    this.target = hit.transform;
                    Debug.Log("O ALVO EH " + this.target);
                }
                else
                {
                    this.target = null;
                }

            }
            else
            {
                this.target = null;
            }
        }
        else
        {
            this.target = null;
        }
    }

}
