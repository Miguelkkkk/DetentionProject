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

    private Transform target;

    void Update()
    {
        Search();
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
    }
    private void Search()
    {
        Collider2D colider = Physics2D.OverlapCircle(this.transform.position, this.viewRadius);
        this.target = colider.transform;
        if (colider != null)
        {
            this.target = colider.transform;
            Debug.Log("O ALVO EH " + this.target);
        }
        else
        {
            this.target = null;
        }
    }

}
