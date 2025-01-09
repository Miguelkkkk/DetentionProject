using UnityEngine.AI;
using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    protected NavMeshAgent agent;

    [Header("Enemy Movement Settings")]
    public float detectionRadius = 5f;
    public LayerMask targetLayer;

    protected Transform target;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false; // Adapt for 2D
        agent.updateRotation = false;
    }

    protected virtual void Update()
    {
        DetectTarget();
        if (target != null)
        {
            MoveTowardsTarget();
        }
    }

    protected void DetectTarget()
    {
        Collider2D detectedTarget = Physics2D.OverlapCircle(transform.position, detectionRadius, targetLayer);
        target = detectedTarget != null ? detectedTarget.transform : null;
    }

    protected virtual void MoveTowardsTarget()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}