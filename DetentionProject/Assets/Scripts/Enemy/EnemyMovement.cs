using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float minDistance;

    [SerializeField]
    private Rigidbody2D rigidbody;

    [SerializeField]
    private Animator animator;

    //[SerializeField]
    //private SpriteRenderer spriteRenderer;

    void Update()
    {
        Vector2 targetPos = this.target.position;
        Vector2 currentPos = this.transform.position;
        float distance = Vector2.Distance(currentPos, targetPos);

        if (distance >= this.minDistance) 
        {
            Vector2 direction = targetPos - currentPos;
            direction = direction.normalized;

            this.rigidbody.velocity = (this.speed * direction);
            this.animator.SetBool("DownWalk", true);
        }
        else
        {
            this.rigidbody.velocity = Vector2.zero;
            this.animator.SetBool("DownWalk", false);
        }
       
        //if(this.rigidbody.velocity.x > 0f)///direita
        //{
        //}

    }
}
